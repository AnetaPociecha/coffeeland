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
import java.util.concurrent.TimeUnit;

import static org.junit.Assert.*;

public class ProfileTest extends AccountTest implements Fillable {
    private static Configurator configurator;
    private static WebDriver driver;
    private static final String PROFILE_URL = "http://localhost:50970/myaccount";

    @BeforeClass
    public static void enableBrowser(){

        configurator = new ChromeConfigurator();
        driver = configurator.getBrowser();
    }

    private void moveToProfileCard(){
        //System.out.println(driver.getCurrentUrl());
        if(!driver.getCurrentUrl().equals(PROFILE_URL))
            redirectToMyAccount(driver);
        driver.findElement(By.xpath("//a[@id='profile']")).click();
    }

    @Test
    //@Ignore //NIE działa - coś sie  dzieje za szybko...
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

        driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);

        savedData = driver.findElement(By.xpath("//div[@class='row']/div[@class='col-12 border']"));
        while(!mail.equals(savedData.findElement(By.xpath(".//div[@class='col-12 p-3']")).getText()))
            savedData = driver.findElement(By.xpath("//div[@class='row']/div[@class='col-12 border']"));
        if(mail.equals(savedData.findElement(By.xpath(".//div[@class='col-12 p-3']")).getText())){
            restoreUserData();
        } else {
            restoreUserData();
            fail();
        }





    }

    @Test
    public void editDataBad(){
        moveToProfileCard();
        String imie = "!!!";
        String nazwisko = "???";
        String mail = "pff..";

        WebElement edit = driver.findElement(By.xpath("//button[text()='Edit']"));
        WebElement save;
        WebElement cancel;

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

        cancel = driver.findElement(By.xpath("//button[text()='Cancel']"));
        cancel.click();
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
        if(driver.getCurrentUrl().equals(AccountTest.HTTP_LOCALHOST +"myaccount") &&
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

    private void restoreUserData(){

        driver.findElement(By.xpath("//button[text()='Edit']")).click();
        Map<String, WebElement> inputs = getInputFields();
        inputs.get("firstName").clear();
        inputs.get("firstName").sendKeys(AccountTest.testName);

        inputs.get("lastName").clear();
        inputs.get("lastName").sendKeys(AccountTest.testSurname);

        inputs.get("mail").clear();
        inputs.get("mail").sendKeys(AccountTest.testMail);

        driver.findElement(By.xpath("//div[@class='col-6 pt-3']//button")).click();

    }

    @AfterClass
    public static void disableBrowser(){
        driver.quit();
    }
}
