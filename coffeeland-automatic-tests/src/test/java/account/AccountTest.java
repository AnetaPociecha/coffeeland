package account;

import configurator.ChromeConfigurator;
import configurator.Configurator;
import org.junit.BeforeClass;
import org.junit.Ignore;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import static org.junit.Assert.assertEquals;

public class AccountTest {

    public static final String HTTP_LOCALHOST = "http://localhost:8080/";
    private static Configurator configurator;
    private static WebDriver driver;

    @BeforeClass
    public static void enableBrowser(){
        configurator = new ChromeConfigurator();
        driver = configurator.getBrowser();
    }



    protected String redirectToMyAccount(WebDriver driver){
        WebElement signIn;
        driver.get(HTTP_LOCALHOST);

        signIn = driver.findElement(By.xpath("//a[@href='/signin']"));
        if(signIn.getText().contains("Sign in")){
            signIn.click();
        }
        driver.findElement(By.xpath("//input[@id='singInEmail']")).sendKeys("ann");
        //driver.findElement(By.xpath("//input[@id='signInPassword']"));
        driver.findElement(By.xpath("//button[text()='Sign in']")).click();

        driver.findElement(By.xpath("//a[@href='/myaccount']")).click();

        return driver.getCurrentUrl();
    }

    @Test
    @Ignore
    public void testRedirectionToAccount(){
        assertEquals(redirectToMyAccount(driver),HTTP_LOCALHOST+"myaccount");
        //assertEquals(driver.findElement(By.xpath("//h3[text()='Address book']")).getText(), "Address book");

    }
}
