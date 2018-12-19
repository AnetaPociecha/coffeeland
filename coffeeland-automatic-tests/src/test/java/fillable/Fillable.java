package fillable;

import org.openqa.selenium.WebElement;

import java.util.Map;

public interface Fillable {
    Map<String, WebElement> getInputFields();
}
