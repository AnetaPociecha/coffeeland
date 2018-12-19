package shopping;

import account.AccountTest;
import configurator.ChromeConfigurator;
import configurator.Configurator;
import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.interactions.Action;
import org.openqa.selenium.interactions.Actions;

import java.util.List;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertTrue;
import static org.junit.Assert.fail;

public class FilterTest {
    private static Configurator configurator;
    private static WebDriver driver;

    @BeforeClass
    public static void enableBrowser(){
        configurator = new ChromeConfigurator();
        driver = configurator.getBrowser();
    }

    private void connect(){
        driver.get(AccountTest.HTTP_LOCALHOST);
    }

    @Test
    public void filterbyCategory(){
        connect();
        String chosenCategory = "Blends";
        WebElement checkboxBlends = driver.findElement(By.xpath("//input[@id='Blends']"));
        List<WebElement> filteredResults;
        checkboxBlends.click();

        filteredResults = driver.findElements(By.xpath("//div[@class='row pt-3 pr-5 pl-3 pb-5']/div/div[@class='border p-2']"));

        for (WebElement record : filteredResults){
            assertEquals(chosenCategory, record.findElement(By.xpath("./p[@class='text-center small']")).getText());
        }
        //checkboxBlends.click();

    }

    @Test
    public void unfilterByCategory(){
        connect();
        WebElement checkboxBlends = driver.findElement(By.xpath("//input[@id='Blends']"));
        List<WebElement> resultsBefore;
        List<WebElement> resultsAfter;
        int sizeBefore;
        int sizeAfter;

        checkboxBlends.click();
        resultsBefore = driver.findElements(By.xpath("//div[@class='row pt-3 pr-5 pl-3 pb-5']/div/div[@class='border p-2']"));
        //if(unfilteringCanWork(resultsBefore)){
            sizeBefore = resultsBefore.size();
            checkboxBlends.click();
            resultsAfter = driver.findElements(By.xpath("//div[@class='row pt-3 pr-5 pl-3 pb-5']/div/div[@class='border p-2']"));
            sizeAfter = resultsAfter.size();

            //System.out.println("Size: after "+sizeAfter+"  before "+sizeBefore);
            assertTrue(sizeAfter > sizeBefore);
        //}
        //else{
         //   System.err.println("Cannot test, only 1 category available");
         //   fail();
       // }
       // checkboxBlends.click();

    }

    @Test
    public void filterByPrice(){
        connect();
        WebElement minimumPrice = driver.findElement(By.xpath("//input[@id='selectedMinPrice']"));
        WebElement maximumPrice = driver.findElement(By.xpath("//input[@id='selectedMaxPrice']"));
        List<WebElement> resultsBefore = driver.findElements(By.xpath("//div[@class='row pt-3 pr-5 pl-3 pb-5']/div/div[@class='border p-2']"));
        List<WebElement> resultsAfter;

        Actions move = new Actions(driver);
        Action minMove = move.dragAndDropBy(minimumPrice, 30, 0).build();
        Action maxMove = move.dragAndDropBy(maximumPrice, -30, 0).build();

        double modifiedMinValue = Double.parseDouble(minimumPrice.getAttribute("value"))/100;
        double modifiedMaxValue = Double.parseDouble(maximumPrice.getAttribute("value"))/100;

        minMove.perform();
        maxMove.perform();

        resultsAfter = driver.findElements(By.xpath("//div[@class='row pt-3 pr-5 pl-3 pb-5']/div/div[@class='border p-2']"));

        for(WebElement result : resultsAfter){
            double price = Double.parseDouble(result.findElement(By.xpath("./p[@class='text-center']")).getText().substring(2));//?
            assertTrue(price >= modifiedMinValue);
            assertTrue(price <= modifiedMaxValue);
            System.out.println("Prices: "+modifiedMinValue+" "+modifiedMaxValue);
        }



    }

    private boolean unfilteringCanWork(List<WebElement> results){
        if(results != null && results.size() > 1) {
            String firstRecord = results.get(0).findElement(By.xpath("./p[@class='text-center small']")).getText();
            for (WebElement element : results) {
                if(!element.findElement(By.xpath("./p[@class='text-center small']")).getText().equals(firstRecord))
                    return true;
            }
        }

        return false;
    }

    @AfterClass
    public static void disableBrowser(){
        driver.quit();
    }
}
