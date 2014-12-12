SeleniumExample - Using Selenium with C#.NET
===============

This solution demonstrates automated testing web pages with Selenium and C#.NET. It can also be used as a template for new Selenium test projects.

Three tests are included that run tests on the publically available demo website Altoro Mutual available at http://demo.testfire.net/. The tests test logging onto the website and transferring an amount between two accounts.

The tests can be executed on three browsers: Firefox, Chrome and Internet Explorer. The driver can be selected using 
the appsetting *DriverToUse* in the app.config file. To run the tests on Internet Explorer 11, the registry must be updated first so that the driver can maintain a connection to the browser. Import the registry file [configure_ie_11_for_selenium_iedriverserver.reg](https://github.com/atosorigin/SeleniumExample/blob/master/configure_ie_11_for_selenium_iedriverserver.reg) to achieve this. 

To run the Selenium tests, download the solution and run the NUnit tests. All selenium dependencies are included in the solution. Run the NUnit tests using Resharper ( http://www.jetbrains.com/resharper ), NUnit ( http://www.nunit.org ) or ContinuousTests ( http://continuoustests.com ).

The tests are structured according to the [Page Object Pattern](https://code.google.com/p/selenium/wiki/PageObjects).

Out of the box Selenium supports locating elements using the element id or an xpath selector. The extension method *FindElementByJQuery* has been added to SeleniumExample with which elements can be located using a more versatile JQuery selector. Example:

    _driver.FindElementByJQuery("input[name='btnSubmit']")

Alexander van Trijffel
