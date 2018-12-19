package account;

import configurator.ChromeConfigurator;
import configurator.Configurator;
import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import java.util.List;

import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertNull;
import static org.junit.Assert.assertTrue;

public class OrdersTest extends AccountTest{
    private static Configurator configurator;
    private static WebDriver driver;
    private String orders = "//div[@class='col-12']/div[@class='border m-3 p-3 col-12']";

    private void moveToOrdersCard(){
        redirectToMyAccount(driver);
        driver.findElement(By.xpath("//a[@id='orders']")).click();
    }

    @BeforeClass
    public static void enableBrowser(){
        configurator = new ChromeConfigurator();
        driver = configurator.getBrowser();
    }

    @Test
    public void canComplainOnlyCompleted(){
        moveToOrdersCard();

        List<WebElement> complains = driver.findElements(By.xpath(orders));

        for(WebElement element: complains){
            WebElement complainButton = element.findElement(By.xpath(".//button[text()='Complain']"));
            if(complainButton.isEnabled()){
               assertNotNull(element.findElement(By.xpath(".//div[text()='Completed']")));
            }
            else{
                assertNotNull(element.findElement(By.xpath(".//div[text()='Processing']")));
            }
        }

    }

    @Test
    public void makeComplain(){
        moveToOrdersCard();
        WebElement ableToComplain = driver.findElement(By.xpath(orders+"/div[text()='Close date']/..//button[text()='Complain']"));
        WebElement complainInput;
        ableToComplain.click();

        complainInput = driver.findElement(By.xpath("//textarea"));

        complainInput.sendKeys("");

        assertTrue(complainInput.getAttribute("class").contains("is-invalid"));

        complainInput.sendKeys("qw.ertyuiopasd.fghjklz.xcvbnm.");

        assertTrue(!complainInput.getAttribute("class").contains("is-invalid"));

    }


    @AfterClass
    public static void disableBrowser(){
        driver.quit();
    }


}
