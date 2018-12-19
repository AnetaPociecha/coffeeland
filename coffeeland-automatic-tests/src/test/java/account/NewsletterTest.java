package account;

import configurator.ChromeConfigurator;
import configurator.Configurator;
import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.NoSuchElementException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import static org.junit.Assert.*;

public class NewsletterTest extends AccountTest{
    private static Configurator configurator;
    private static WebDriver driver;

    @BeforeClass
    public static void enableBrowser(){
        configurator = new ChromeConfigurator();
        driver = configurator.getBrowser();
    }

    private void moveToNewsletter(){
        redirectToMyAccount(driver);
        driver.findElement(By.xpath("//a[@id='newsletter']")).click();
    }

    @Test
    public void signOutFromNewsletter(){
        moveToNewsletter();
        WebElement signOut = driver.findElement(By.xpath("//button[text()='Sign out']"));
        //WebElement locationOfMail = driver.findElement(By.xpath("//h3[text()='Newsletter']"));
        //WebElement currentMail = locationOfMail.findElement(By.xpath("./div/div/div/div[@class='col-12 p-1]"));

        signOut.click();

        //assertNull(driver.findElement(By.xpath("//h3[text()='Newsletter']")));
        assertNotNull(driver.findElement(By.xpath("//input[@id='newsletterEmail']")));


    }

    @Test
    public void signInBadData(){
        moveToNewsletter();
        signOut();
        WebElement signIn = driver.findElement(By.xpath("//button[text()='Sign in']"));
        WebElement emailField = driver.findElement(By.xpath("//input[@id='newsletterEmail']"));

        emailField.sendKeys("zlyemail");

        signIn.click();

        assertTrue(driver.findElement(By.xpath("//input[@id='newsletterEmail']")).getAttribute("class").contains("is-invalid"));
        assertTrue(!driver.findElement(By.xpath("//button[text()='Sign in']")).isEnabled());

        emailField.clear();
    }

    @Test
    public void signInCorrectData(){
        moveToNewsletter();
        signOut();
        String email = "dobry@email.com";
        WebElement signIn = driver.findElement(By.xpath("//button[text()='Sign in']"));
        WebElement emailField = driver.findElement(By.xpath("//input[@id='newsletterEmail']"));

        emailField.sendKeys(email);

        signIn.click();

        assertNotNull(driver.findElement(By.xpath("//button[text()='Sign out']")));
        assertEquals(email, driver.findElement(By.xpath("//div[@class='col-7 p-3']/div[text()='"+email+"']")).getText());
    }

    private void signOut(){
        WebElement signOut;

        try {
            signOut = driver.findElement(By.xpath("//button[text()='Sign out']"));
            signOut.click();
        }
        catch (NoSuchElementException e){
            System.err.println("Currently signed out.");
        }
    }

    @AfterClass
    public static void disableBrowser(){
      driver.quit();
    }
}
