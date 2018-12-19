package configurator;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.remote.DesiredCapabilities;

import java.io.File;

public class ChromeConfigurator implements Configurator {
    public WebDriver getBrowser() {
        System.setProperty("webdriver.chrome.driver", "/usr/lib/chromium-browser/chromedriver");
        ChromeOptions options = new ChromeOptions();
        options.addExtensions(new File("/home/r-toor/IdeaProjects/coffeeland-automatic-tests/resources/plugins/redux-devtools216.crx"));
        DesiredCapabilities capabilities = new DesiredCapabilities();
        capabilities.setCapability(ChromeOptions.CAPABILITY, options);
        WebDriver driver = new ChromeDriver(capabilities);

        return driver;
    }
}
