package account;

import configurator.ChromeConfigurator;
import configurator.Configurator;
import fillable.Fillable;
import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Ignore;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;

public class AddressBookTest extends AccountTest implements Fillable {

    private static Configurator configurator;
    private static WebDriver driver;

    @BeforeClass
    public static void enableBrowser(){
        configurator = new ChromeConfigurator();
        driver = configurator.getBrowser();
    }

    private void redirectToAddressBook(){
        redirectToMyAccount(driver);
        driver.findElement(By.xpath("//a[@id='addressbook']")).click();
    }

    @Test
    public void checkRemovingAddress(){
        redirectToAddressBook();
        List<WebElement> elements = driver.findElements(By.xpath("//div/div/div/button[text()='Remove']"));
        int lengthBefore = elements.size();

        elements.get(0).click();

        List<WebElement> elementsAfter = driver.findElements(By.xpath("//div/div/div/button[text()='Remove']"));
        assertEquals(lengthBefore-1, elementsAfter.size());
    }

    @Test
    @Ignore
    public void checkEditingAddress(){
        redirectToAddressBook();
        List<WebElement> elements = driver.findElements(By.xpath("//div/div/div/button[text()='Edit']"));

        elements.get(0).click();

        WebElement saveButton = driver.findElement(By.xpath("//div/div/div/button[text()='Save']"));
        assertNotNull(saveButton);
    }

    @Test
    public void checkCorrectData(){
        redirectToAddressBook();
        Map<String, WebElement> inputs;
        WebElement addNewAddress = driver.findElement(By.xpath("//div[@class='pb-5 pr-3 pl-3 pt-4 text-center']//button[text()='Add new address']"));
        WebElement saveKey;

        addNewAddress.click();

        inputs = getInputFields();
        saveKey = driver.findElement(By.xpath("//div[@class='pb-5 pr-3 pl-3 pt-4']//button[text()='Save']"));

        inputs.get("country").sendKeys("Polska");
        inputs.get("city").sendKeys("Cracow");
        inputs.get("zip").sendKeys("22-222");
        inputs.get("street").sendKeys("Sliczna");
        inputs.get("build").sendKeys("155"); // akceptuje tylko cyfry
        inputs.get("apartment").sendKeys("100");

        assertTrue(!inputs.get("country").getAttribute("class").contains("is-invalid"));
        assertTrue(!inputs.get("city").getAttribute("class").contains("is-invalid"));
        assertTrue(!inputs.get("zip").getAttribute("class").contains("is-invalid"));
        assertTrue(!inputs.get("street").getAttribute("class").contains("is-invalid"));
        assertTrue(!inputs.get("build").getAttribute("class").contains("is-invalid"));
        assertTrue(!inputs.get("apartment").getAttribute("class").contains("is-invalid"));

        assertTrue(saveKey.isEnabled());


    }

    @Test
    public void checkBadData(){
        redirectToAddressBook();
        Map<String, WebElement> inputs;

        WebElement addNewAddress = driver.findElement(By.xpath("//div[@class='pb-5 pr-3 pl-3 pt-4 text-center']//button[text()='Add new address']"));
        WebElement saveKey;
        addNewAddress.click();

        inputs = getInputFields();
        saveKey = driver.findElement(By.xpath("//div[@class='pb-5 pr-3 pl-3 pt-4']//button[text()='Save']"));

        inputs.get("country").sendKeys("!Dupa");
        inputs.get("city").sendKeys("23");
        inputs.get("zip").sendKeys("222");
        inputs.get("street").sendKeys("@?!");
        inputs.get("build").sendKeys("()"); // akceptuje tylko cyfry
        inputs.get("apartment").sendKeys("()");

        saveKey.click();

        assertTrue(inputs.get("country").getAttribute("class").contains("is-invalid"));
        assertTrue(inputs.get("city").getAttribute("class").contains("is-invalid"));
        assertTrue(inputs.get("zip").getAttribute("class").contains("is-invalid"));
        assertTrue(inputs.get("street").getAttribute("class").contains("is-invalid"));
        assertTrue(inputs.get("build").getAttribute("class").contains("is-invalid"));
        assertTrue(inputs.get("apartment").getAttribute("class").contains("is-invalid"));

        assertTrue(!saveKey.isEnabled());
    }

    @Test
    public void cancelOrAddNewAddress(){
        redirectToAddressBook();
        WebElement addNewAddress = driver.findElement(By.xpath("//div[@class='pb-5 pr-3 pl-3 pt-4 text-center']//button[text()='Add new address']"));
        WebElement cancel;
        List<WebElement> inputs;

        addNewAddress.click();

        cancel = driver.findElement(By.xpath("//div[@class='pb-5 pr-3 pl-3 pt-4']//button[text()='Cancel']"));
        inputs = driver.findElements(By.xpath("//div[@class='pb-5 pr-3 pl-3 pt-4']//input"));

        cancel.click();

        assertNotNull(addNewAddress);
        assertNotNull(inputs);
    }

    public Map<String, WebElement> getInputFields(){
        if(driver.getCurrentUrl().equals(AccountTest.HTTP_LOCALHOST+"myaccount") &&
                driver.findElement(By.xpath("//a[@href='/myaccount#addressbook']")).getText().equals("Address book")) {
            String pathToInputs = "//div[@class='pb-5 pr-3 pl-3 pt-4']//input";
            Map<String, WebElement> fields = new HashMap();
            fields.put("country", driver.findElement(By.xpath(pathToInputs + "[@id='country']")));
            fields.put("city", driver.findElement(By.xpath(pathToInputs + "[@id='city']")));
            fields.put("zip", driver.findElement(By.xpath(pathToInputs + "[@id='ZIPCode']")));
            fields.put("street", driver.findElement(By.xpath(pathToInputs + "[@id='street']")));
            fields.put("build", driver.findElement(By.xpath(pathToInputs + "[@id='buildingNumber']")));
            fields.put("apartment", driver.findElement(By.xpath(pathToInputs + "[@id='apartmentNumber']")));

            return fields;
        }
        return null;
    }


    //todo checking categories

    //todo clicking on MyAccount may better lead to link myaccount#addressbook

    @AfterClass
    public static void disableBrowser(){
        driver.quit();
    }
}
