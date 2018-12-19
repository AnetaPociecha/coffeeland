import account.AccountTest;
import configurator.ChromeConfigurator;
import configurator.Configurator;
import fillable.Fillable;
import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Ignore;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.NoSuchElementException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import java.util.HashMap;
import java.util.Map;

import static org.junit.Assert.*;

public class SignTest implements Fillable {
    private static Configurator configurator;
    private static WebDriver driver;

    @BeforeClass
    public static void enableBrowser(){
        configurator = new ChromeConfigurator();
        driver = configurator.getBrowser();
    }



    private WebElement signOut(){
        WebElement signOut;

        driver.get(AccountTest.HTTP_LOCALHOST);
        signOut = driver.findElement(By.xpath("//a[@href='/signin']"));
        if(signOut.getText().contains("Sign out")){
            signOut.click();
        }
        return signOut;
    }

    private WebElement signIn(){
        WebElement signIn;

        driver.get(AccountTest.HTTP_LOCALHOST);
        signIn = driver.findElement(By.xpath("//a[@href='/signin']"));
        if(signIn.getText().contains("Sign in")){
            signIn.click();
        }
        return signIn;
    }

    private void signOutIn(){
        WebElement sign;

        driver.get(AccountTest.HTTP_LOCALHOST);
        sign = driver.findElement(By.xpath("//a[@href='/signin']"));
        sign.click();
        sign.click();
    }

    @Test
    public void testLogout(){
        WebElement signOut;
        WebElement signIn;
        String signOutText;
        String signInText;

        driver.get(AccountTest.HTTP_LOCALHOST);

        signOut = driver.findElement(By.xpath("//a[@href='/signin']"));
        signOutText = signOut.getText();
        if(signOutText.toLowerCase().contains("out")) {
            signOut.click();

            signIn = driver.findElement(By.xpath("//a[@href='/signin']"));
            signInText = signIn.getText();

            assertNotEquals(signInText, signOutText);

            try {
                assertNull(driver.findElement(By.xpath("//a[@href='/myaccount']")));
                assertEquals(1, 0);
            } catch (NoSuchElementException e) {
                assertEquals(1, 1);
            }
        }else{
            System.err.println("Unable to test..");
        }

    }

    @Test
    public void signInCorrectData(){

    }

    @Test
    public void signInCorrectNotExistingData(){

    }

    @Test
    public void checkTermsOfPrivacy(){
        signIn();
        WebElement linkToTermsContent = driver.findElement(By.xpath("//a[text()='Agree to terms and conditions']"));
        WebElement OK;

        linkToTermsContent.click();
        OK = driver.findElement(By.xpath("//button[text()='OK']"));

        assertNotNull(driver.findElement(By.xpath("//div[@class='ReactModal__Overlay ReactModal__Overlay--after-open']")));

        OK.click();
        try {
            assertNull(driver.findElement(By.xpath("//div[@class='ReactModalPortal__Overlay ReactModal__Overlay--after-open']")));
        }catch(NoSuchElementException e){
            assertTrue(true);
        }
    }

    @Test
    @Ignore
    public void signInBadData(){
        signOutIn();
        WebElement alert;
        WebElement signIn = driver.findElement(By.xpath("//button[text()='Sign in']"));

        Map<String, WebElement> inputs = getInputFields();

        inputs.get("signMail").sendKeys("nonexisting");
        inputs.get("signPassword").sendKeys("nonexisting");

        signIn.click();

        alert = driver.findElement(By.xpath("//div[@class='alert alert-danger text-center']"));

        assertEquals(driver.getCurrentUrl(), AccountTest.HTTP_LOCALHOST+"signin");
        assertNotNull(alert);

    }

    @Test
    public void registerCorrectData(){
        signOutIn();
        WebElement register = driver.findElement(By.xpath("//button[text()='Register']"));

        Map<String, WebElement> inputs = getInputFields();

        inputs.get("firstName").sendKeys("Imie");
        inputs.get("lastName").sendKeys("Nazwisko");
        inputs.get("registerMail").sendKeys("mail@mail.com");
        inputs.get("registerPassword").sendKeys("Qwertyuiop1");
        inputs.get("repeatedPassword").sendKeys("Qwertyuiop1");
        inputs.get("checkBox").click();

        register.click();

        assertTrue(!inputs.get("firstName").getAttribute("class").contains("is-invalid"));
        assertTrue(!inputs.get("lastName").getAttribute("class").contains("is-invalid"));
        assertTrue(!inputs.get("registerMail").getAttribute("class").contains("is-invalid"));
        assertTrue(!inputs.get("registerPassword").getAttribute("class").contains("is-invalid"));
        assertTrue(!inputs.get("repeatedPassword").getAttribute("class").contains("is-invalid"));

    }

    @Test
    public void registerBadData(){
        signOutIn();
        WebElement register = driver.findElement(By.xpath("//button[text()='Register']"));

        Map<String, WebElement> inputs = getInputFields();

        inputs.get("firstName").sendKeys("99");
        inputs.get("lastName").sendKeys("99");
        inputs.get("registerMail").sendKeys("99");
        inputs.get("registerPassword").sendKeys("99");
        inputs.get("repeatedPassword").sendKeys("999");

        register.click();

        assertTrue(inputs.get("firstName").getAttribute("class").contains("is-invalid"));
        assertTrue(inputs.get("lastName").getAttribute("class").contains("is-invalid"));
        assertTrue(inputs.get("registerMail").getAttribute("class").contains("is-invalid"));
        assertTrue(inputs.get("registerPassword").getAttribute("class").contains("is-invalid"));
        assertTrue(inputs.get("repeatedPassword").getAttribute("class").contains("is-invalid"));
    }

    public Map<String, WebElement> getInputFields() {
        if(driver.getCurrentUrl().equals(AccountTest.HTTP_LOCALHOST+"signin")) {
            Map<String, WebElement> inputs = new HashMap();
            inputs.put("signMail", driver.findElement(By.xpath("//input[@id='singInEmail']")));
            inputs.put("signPassword", driver.findElement(By.xpath("//input[@id='signInPassword']")));
            inputs.put("firstName", driver.findElement(By.xpath("//input[@id='firstName']")));
            inputs.put("lastName", driver.findElement(By.xpath("//input[@id='lastName']")));
            inputs.put("registerMail", driver.findElement(By.xpath("//input[@id='registerEmail']")));
            inputs.put("registerPassword", driver.findElement(By.xpath("//input[@id='registerPassword']")));
            inputs.put("repeatedPassword", driver.findElement(By.xpath("//input[@id='repeatedPassword']")));
            inputs.put("checkBox", driver.findElement(By.xpath("//input[@type='checkbox']")));
            return inputs;
        }
        return null;
    }

    @AfterClass
    public static void disableBrowser(){
      driver.quit();
    }
}
