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

import java.util.concurrent.TimeUnit;

import static org.junit.Assert.*;

public class NewsletterTest extends AccountTest{
    private static Configurator configurator;
    private static WebDriver driver;
    private static final String NEWSLETTER_URL = "http://localhost:50970/myaccount";


    @BeforeClass
    public static void enableBrowser(){
        configurator = new ChromeConfigurator();
        driver = configurator.getBrowser();
    }

    private void moveToNewsletter(){
        if(!driver.getCurrentUrl().equals(NEWSLETTER_URL))
            redirectToMyAccount(driver);
        driver.findElement(By.xpath("//a[@id='newsletter']")).click();
    }

    @Test
    public void signInBadData(){
        moveToNewsletter();
        signOut();
        WebElement signIn = driver.findElement(By.xpath("//button[text()='Sign in']"));
        WebElement emailField = driver.findElement(By.xpath("//input[@id='newNewsletterEmail']"));

        emailField.sendKeys("zlyemail");

        signIn.click();
        //driver.manage().timeouts().implicitlyWait(100, TimeUnit.MILLISECONDS);

        assertTrue(driver.findElement(By.xpath("//input[@id='newNewsletterEmail']")).getAttribute("class").contains("is-invalid"));
        assertTrue(!driver.findElement(By.xpath("//button[text()='Sign in']")).isEnabled());

        emailField.clear();
    }



    @Test
    public void signInCorrectData(){
        moveToNewsletter();
        signOut();
        String email = "dobry@email.com";
        WebElement signIn = driver.findElement(By.xpath("//button[text()='Sign in']"));
        WebElement emailField = driver.findElement(By.xpath("//input[@id='newNewsletterEmail']"));

        emailField.sendKeys(email);

        signIn.click();
        driver.manage().timeouts().implicitlyWait(100, TimeUnit.MILLISECONDS);

        assertNotNull(driver.findElement(By.xpath("//button[text()='Sign out']")));
        assertEquals(email, driver.findElement(By.xpath("//div[@class='col-7 p-3']/div[text()='"+email+"']")).getText());
    }

    @Test
    public void signOutFromNewsletter(){
        moveToNewsletter();
        //signInCorrectData();
        signIn();
        WebElement signOut = driver.findElement(By.xpath("//button[text()='Sign out']"));
        //WebElement locationOfMail = driver.findElement(By.xpath("//h3[text()='Newsletter']"));
        //WebElement currentMail = locationOfMail.findElement(By.xpath("./div/div/div/div[@class='col-12 p-1]"));

        signOut.click();
        driver.manage().timeouts().implicitlyWait(100, TimeUnit.MILLISECONDS);

        //assertNull(driver.findElement(By.xpath("//h3[text()='Newsletter']")));
        assertNotNull(driver.findElement(By.xpath("//input[@id='newNewsletterEmail']")));


    }

    private void signIn(){
        String email = "dobry@email.com";
        try {
            WebElement signIn = driver.findElement(By.xpath("//button[text()='Sign in']"));
            WebElement emailField = driver.findElement(By.xpath("//input[@id='newNewsletterEmail']"));

            emailField.sendKeys(email);

            signIn.click();
        }catch(Exception e){
            System.err.println("Currently signed in");
            signOut();
            driver.manage().timeouts().implicitlyWait(100, TimeUnit.MILLISECONDS);

            WebElement signIn = driver.findElement(By.xpath("//button[text()='Sign in']"));
            WebElement emailField = driver.findElement(By.xpath("//input[@id='newNewsletterEmail']"));
            emailField.sendKeys(email);
            signIn.click();

        }
        driver.manage().timeouts().implicitlyWait(100, TimeUnit.MILLISECONDS);
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
