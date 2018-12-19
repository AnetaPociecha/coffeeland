package account;

import configurator.ChromeConfigurator;
import configurator.Configurator;
import fillable.Fillable;
import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import java.util.HashMap;
import java.util.Map;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;

public class ProfileTest extends AccountTest implements Fillable {
    private static Configurator configurator;
    private static WebDriver driver;

    @BeforeClass
    public static void enableBrowser(){
        configurator = new ChromeConfigurator();
        driver = configurator.getBrowser();
    }

    private void moveToProfileCard(){
        redirectToMyAccount(driver);
        driver.findElement(By.xpath("//a[@id='profile']")).click();
    }

    @Test
    public void editDataCorrect(){
        moveToProfileCard();
        String imie = "Imie";
        String nazwisko = "Nazwisko";
        String mail = "mail@mail.com";

        WebElement edit = driver.findElement(By.xpath("//button[text()='Edit']"));
        WebElement save;
        WebElement savedData;

        edit.click();

        Map<String, WebElement> inputs = getInputFields();
        assertNotNull(inputs);

        inputs.get("firstName").clear();
        inputs.get("firstName").sendKeys(imie);

        inputs.get("lastName").clear();
        inputs.get("lastName").sendKeys(nazwisko);

        inputs.get("mail").clear();
        inputs.get("mail").sendKeys(mail);

        save = driver.findElement(By.xpath("//div[@class='col-6 pt-3']//button"));
        save.click();

        savedData = driver.findElement(By.xpath("//div[@class='row']/div[@class='col-12 border']"));

        assertEquals(savedData.findElement(By.xpath(".//div[@class='col-12 p-3']")).getText(), mail);
        assertEquals(savedData.findElement(By.xpath(".//div[@class='col-12 pt-3 pl-3 pr-3']")).getText(),imie+" "+nazwisko);

    }

    @Test
    public void editDataBad(){
        moveToProfileCard();
        String imie = "!!!";
        String nazwisko = "???";
        String mail = "pff..";

        WebElement edit = driver.findElement(By.xpath("//button[text()='Edit']"));
        WebElement save;

        edit.click();

        Map<String, WebElement> inputs = getInputFields();
        assertNotNull(inputs);

        inputs.get("firstName").clear();
        inputs.get("firstName").sendKeys(imie);

        inputs.get("lastName").clear();
        inputs.get("lastName").sendKeys(nazwisko);

        inputs.get("mail").clear();
        inputs.get("mail").sendKeys(mail);

        save = driver.findElement(By.xpath("//div[@class='col-6 pt-3']//button"));

        assertTrue(inputs.get("firstName").getAttribute("class").contains("is-invalid"));
        assertTrue(inputs.get("lastName").getAttribute("class").contains("is-invalid"));
        assertTrue(inputs.get("mail").getAttribute("class").contains("is-invalid"));
        assertTrue(!save.isEnabled());

    }

    @Test
    public void cancelData(){
        moveToProfileCard();
        String mailBefore = driver.findElement(By.xpath("//div[@class='row']/div[@class='col-12 border']//div[@class='col-12 p-3']")).getText();
        String nameBefore = driver.findElement(By.xpath("//div[@class='row']/div[@class='col-12 border']//div[@class='col-12 pt-3 pl-3 pr-3']")).getText();
        String mailAfter;
        String nameAfter;

        WebElement edit = driver.findElement(By.xpath("//button[text()='Edit']"));
        WebElement cancel;

        edit.click();

        Map<String, WebElement> inputs = getInputFields();
        assertNotNull(inputs);

        inputs.get("firstName").clear();
        inputs.get("firstName").sendKeys("ds");

        inputs.get("lastName").clear();
        inputs.get("lastName").sendKeys("ds");

        inputs.get("mail").clear();
        inputs.get("mail").sendKeys("ds");

        cancel = driver.findElement(By.xpath("//div[@class='col-6 text-right pt-3']//button"));
        cancel.click();

        mailAfter = driver.findElement(By.xpath("//div[@class='row']/div[@class='col-12 border']//div[@class='col-12 p-3']")).getText();
        nameAfter = driver.findElement(By.xpath("//div[@class='row']/div[@class='col-12 border']//div[@class='col-12 pt-3 pl-3 pr-3']")).getText();

        assertEquals(mailAfter, mailBefore);
        assertEquals(nameAfter, nameBefore);
    }

    public Map<String, WebElement> getInputFields(){
        if(driver.getCurrentUrl().equals(AccountTest.HTTP_LOCALHOST+"myaccount") &&
                driver.findElement(By.xpath("//a[@href='/myaccount#profile']")).getText().equals("Profile")){
            String pathToInputs = "//div[@class='row p-2 pt-3 border']//input";
            Map<String, WebElement> fields = new HashMap();
            fields.put("firstName", driver.findElement(By.xpath(pathToInputs + "[@id='firstName']")));
            fields.put("lastName", driver.findElement(By.xpath(pathToInputs + "[@id='lastName']")));
            fields.put("mail", driver.findElement(By.xpath(pathToInputs + "[@id='email']")));

            return fields;
        }
        return null;
    }

    @AfterClass
    public static void disableBrowser(){
        driver.quit();
    }
}
