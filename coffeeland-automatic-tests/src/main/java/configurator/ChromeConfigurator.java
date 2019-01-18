package configurator;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.remote.DesiredCapabilities;

import java.io.File;

public class ChromeConfigurator implements Configurator {
    public static final String CHROME_DRIVER = "D:\\chrome-win\\chromedriver.exe";
    public static final String REDUX_DEVTOOLS = "C:\\Users\\Ar\\IdeaProjects\\coffeeland-master\\coffeeland-automatic-tests\\resources\\plugins\\redux-devtools-216.crx";
    public WebDriver getBrowser() {
        System.setProperty("webdriver.chrome.driver", CHROME_DRIVER);
        ChromeOptions options = new ChromeOptions();
        options.addExtensions(new File(REDUX_DEVTOOLS));
        DesiredCapabilities capabilities = new DesiredCapabilities();
        capabilities.setCapability(ChromeOptions.CAPABILITY, options);
        WebDriver driver = new ChromeDriver(capabilities);

        return driver;
    }
}
