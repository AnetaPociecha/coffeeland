package example;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.remote.DesiredCapabilities;

import java.io.File;

public class Example {

    public static void main(String[] args){
        System.setProperty("webdriver.chrome.driver", "/usr/lib/chromium-browser/chromedriver");
        ChromeOptions options = new ChromeOptions();
        //options.addExtensions(new File("/home/r-toor/.config/chromium/Default/Extensions/lmhkpmbekcpmknklioeibfkpmmfibljd/2.16.4_0/js/redux-devtools-extension.js"));
        options.addExtensions(new File("/home/r-toor/IdeaProjects/coffeeland-automatic-tests/resources/plugins/redux-devtools216.crx"));
        DesiredCapabilities capabilities = new DesiredCapabilities();
        capabilities.setCapability(ChromeOptions.CAPABILITY, options);
        WebDriver driver = new ChromeDriver(capabilities);
        driver.get("http://localhost:3000/");

    }
}
