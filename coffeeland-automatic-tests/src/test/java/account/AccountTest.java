package account;

import configurator.ChromeConfigurator;
import configurator.Configurator;
import org.junit.BeforeClass;
import org.junit.Ignore;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import java.util.concurrent.TimeUnit;

import static org.junit.Assert.assertEquals;

public class AccountTest {

    public static final String HTTP_LOCALHOST_ONLOAD = "http://localhost:50970/Client/dist/index.html";
    public static final String HTTP_LOCALHOST = "http://localhost:50970/";
    public static final String testMail = "jane_doe@gmail.com";
    public static final String testPassword = "admin123";
    public static final String testName = "Jane";
    public static final String testSurname = "Doe";



    private static Configurator configurator;
    private static WebDriver driver;

    @BeforeClass
    public static void enableBrowser(){
        configurator = new ChromeConfigurator();
        driver = configurator.getBrowser();
    }



    protected String redirectToMyAccount(WebDriver driver){
        WebElement signIn;
        driver.get(HTTP_LOCALHOST_ONLOAD);
        driver.manage().timeouts().pageLoadTimeout(10, TimeUnit.SECONDS);
            signIn = driver.findElement(By.xpath("//a[@href='/signin']"));

            signIn.click();
            signIn.click();

            driver.findElement(By.xpath("//input[@id='singInEmail']")).sendKeys(testMail);
            driver.findElement(By.xpath("//input[@id='signInPassword']")).sendKeys(testPassword);
            driver.findElement(By.xpath("//button[text()='Sign in']")).click();
            driver.manage().timeouts().pageLoadTimeout(10, TimeUnit.SECONDS);
            while(!driver.getCurrentUrl().equals(HTTP_LOCALHOST)){}
            driver.findElement(By.xpath("//a[@href='/myaccount']")).click();

        return driver.getCurrentUrl();
    }

    @Test
    @Ignore
    public void testRedirectionToAccount(){
        assertEquals(redirectToMyAccount(driver), HTTP_LOCALHOST +"myaccount");
    }
}
