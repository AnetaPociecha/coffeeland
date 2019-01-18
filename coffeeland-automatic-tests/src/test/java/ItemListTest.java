import account.AccountTest;
import configurator.ChromeConfigurator;
import configurator.Configurator;

import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import static org.junit.Assert.assertEquals;


public class ItemListTest {
    private static Configurator configurator;
    private static WebDriver driver;

    @BeforeClass
    public static void enableBrowser(){
        configurator = new ChromeConfigurator();
        driver = configurator.getBrowser();
    }

    @Test
    public void viewInformations(){
        driver.get(AccountTest.HTTP_LOCALHOST_ONLOAD);

        WebElement information = driver.findElement(By.xpath("//a[@href='/information']"));
        information.click();

        assertEquals(driver.getCurrentUrl(), AccountTest.HTTP_LOCALHOST +"information");
    }

    @AfterClass
    public static void disableBrowser(){
        driver.quit();
    }

}
