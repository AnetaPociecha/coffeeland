package cart;

import account.AccountTest;
import configurator.ChromeConfigurator;
import configurator.Configurator;
import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.NoSuchElementException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import java.util.List;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.fail;

//todo po klikaniu wstecz, czy też zwykłym cofnieciu się do localhost:3000, następuje czyszczenie koszyka...
//todo testowanie poldicznej ceny???
public class CartTest {
    private static Configurator configurator;
    private static WebDriver driver;

    @BeforeClass
    public static void enableBrowser(){
        configurator = new ChromeConfigurator();
        driver = configurator.getBrowser();
    }



    protected String redirectToCart(WebDriver driver){
        driver.get(AccountTest.HTTP_LOCALHOST);
        driver.findElement(By.xpath("//a[@href='/cart']")).click();

        return driver.getCurrentUrl();
    }

    @Test
    public void testRedirectionToCart(){
        assertEquals(redirectToCart(driver),AccountTest.HTTP_LOCALHOST+"cart");
    }

    @Test
    public void addToCart(){
        String nameOfCoffee = "Guatemala Antigua";
        driver.get(AccountTest.HTTP_LOCALHOST);
        WebElement coffee = driver.findElement(By.xpath("//div[@class='border p-2']//h6[text()='"+nameOfCoffee+"']"));
        WebElement boughtCoffee;
        String coffeeName = coffee.getText();
        String boughtCoffeeName;


        WebElement addToCart;
        WebElement amountInShop;
        WebElement amountInCart;
        WebElement cart = driver.findElement(By.xpath("//a[@href='/cart']"));
        List<WebElement> commodityInCart;

        int commodityAmountInShop;
        int commodityAmountInCart;

        coffee.click();

        addToCart = driver.findElement(By.xpath("//button[text()='Add to cart']"));

        amountInShop = driver.findElement(By.xpath("//input[@class='form-control input-number']"));
        commodityAmountInShop = Integer.parseInt(amountInShop.getAttribute("value"));

        addToCart.click();

        cart.click();

        amountInCart = driver.findElement(By.xpath("//input[@class='form-control input-number']"));
        commodityAmountInCart = Integer.parseInt(amountInCart.getAttribute("value"));

        commodityInCart = driver.findElements(By.xpath("//div[@class='row p-1 m-2 border']"));

        boughtCoffee = commodityInCart.get(0).findElement(By.xpath(".//div[text()='"+nameOfCoffee+"']"));
        boughtCoffeeName = boughtCoffee.getText();

        assertEquals(commodityInCart.size(), 1);
        assertEquals(coffeeName, boughtCoffeeName);
        assertEquals(commodityAmountInCart,commodityAmountInShop);

    }

    @Test
    public void boundariesOfAmount(){
        driver.get(AccountTest.HTTP_LOCALHOST);

        WebElement coffee = driver.findElement(By.xpath("//div[@class='border p-2']//h6[text()='Guatemala Antigua']"));
        WebElement buttonPlus;
        WebElement buttonMinus;
        WebElement amountInCart;
        int max;
        int tmp1=0, tmp2=1;
        int min;

        coffee.click();

        buttonPlus = driver.findElement(By.xpath("//span[@class='input-group-btn']/button/span[text()='+']"));
        buttonMinus = driver.findElement(By.xpath("//span[@class='input-group-btn']/button/span[text()='-']"));

        amountInCart = driver.findElement(By.xpath("//input[@class='form-control input-number']"));

        while(tmp1 != tmp2){
            buttonPlus.click();
            tmp1 = tmp2;
            tmp2 = Integer.parseInt(amountInCart.getAttribute("value"));
        }

        max = Integer.parseInt(amountInCart.getAttribute("value"));

        tmp1=0; tmp2=1;
        while (tmp1 != tmp2){
            buttonMinus.click();
            tmp1 = tmp2;
            tmp2 = Integer.parseInt(amountInCart.getAttribute("value"));
        }

        min = Integer.parseInt(amountInCart.getAttribute("value"));

        assertEquals(max, 5);
        assertEquals(min, 1);
    }

    @Test
    public void addToCartMultiple(){
        driver.get(AccountTest.HTTP_LOCALHOST);
        WebElement cart = driver.findElement(By.xpath("//a[@href='/cart']"));
        List<WebElement> commodityInCart;
        int amount = 6;
        for(int i=0;i<amount;i++)
            addOneCommodity(i);

        cart.click();

        commodityInCart = driver.findElements(By.xpath("//div[@class='row p-1 m-2 border']"));

        assertEquals(commodityInCart.size(), amount);


    }

    @Test
    public void addAboveMaximum(){
        driver.get(AccountTest.HTTP_LOCALHOST);
        WebElement cart = driver.findElement(By.xpath("//a[@href='/cart']"));
        WebElement amountInCart;
        List<WebElement> commodityInCart;
        int maxAllowed = 5;

        for (int i=0;i<10;i++){
            addOneCommodity(0);
        }

        cart.click();
        commodityInCart = driver.findElements(By.xpath("//div[@class='row p-1 m-2 border']"));
        amountInCart = driver.findElement(By.xpath("//input[@class='form-control input-number']"));

        assertEquals(commodityInCart.size(), 1);
        assertEquals(Integer.parseInt(amountInCart.getAttribute("value")), maxAllowed);

    }

    @Test
    public void removeFromCart(){
        driver.get(AccountTest.HTTP_LOCALHOST);
        WebElement cart = driver.findElement(By.xpath("//a[@href='/cart']"));
        WebElement remove;
        int sizeBefore;
        int sizeAfter;
        List<WebElement> commodity;
        addOneCommodity(0);

        cart.click();
        commodity = driver.findElements(By.xpath("//div[@class='row p-1 m-2 border']"));
        sizeBefore = commodity.size();

        remove = driver.findElement(By.xpath("//button[text()='Remove']"));
        remove.click();
        commodity = driver.findElements(By.xpath("//div[@class='row p-1 m-2 border']"));
        sizeAfter = commodity.size();

        assertEquals(sizeBefore,1);
        assertEquals(sizeAfter, 0);
    }

    @Test
    public void buy(){
        driver.get(AccountTest.HTTP_LOCALHOST);
        WebElement cart = driver.findElement(By.xpath("//a[@href='/cart']"));


        addOneCommodity(0);

        cart.click();

        try {
            driver.findElement(By.xpath("//button[text()='Buy']"));
        }catch (NoSuchElementException e){
            System.err.println("Cannot locate 'Buy' button");
            fail();
        }

    }

    private void addOneCommodity(int index){
        List<WebElement> coffees = driver.findElements(By.xpath("//div[@class='border p-2']//h6"));
        WebElement addToCart;
        if(index < coffees.size()){
            coffees.get(index).click();
            addToCart = driver.findElement(By.xpath("//button[text()='Add to cart']"));
            addToCart.click();
        }
        driver.findElement(By.xpath("//a[@href='/']")).click();
    }



    @AfterClass
    public static void disableBrowser(){
        driver.quit();
    }
}
