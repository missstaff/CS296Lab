

# ZAP Scanning Report

Generated on Sat, 13 Feb 2021 09:57:11

***First Scan***

## Summary of Alerts

| Risk Level    | Number of Alerts |
| ------------- | ---------------- |
| High          | 1                |
| Medium        | 3                |
| Low           | 17               |
| Informational | 10               |

## Alerts

| Name                                                         | Risk Level    | Number of Instances |
| ------------------------------------------------------------ | ------------- | ------------------- |
| Cross Site Scripting (Reflected)                             | High          | 2                   |
| Cross-Domain Misconfiguration                                | Medium        | 4                   |
| Vulnerable JS Library                                        | Medium        | 1                   |
| Cookie Without Secure Flag                                   | Low           | 2                   |
| Cross-Domain JavaScript Source File Inclusion                | Low           | 2238                |
| Incomplete or No Cache-control and Pragma HTTP Header Set    | Low           | 787                 |
| Server Leaks Information via "X-Powered-By" HTTP Response Header Field(s) | Low           | 1116                |
| X-Content-Type-Options Header Missing                        | Low           | 776                 |
| Charset Mismatch                                             | Informational | 1                   |
| Information Disclosure - Suspicious Comments                 | Informational | 4                   |
| Loosely Scoped Cookie                                        | Informational | 4                   |
| Timestamp Disclosure - Unix                                  | Informational | 425                 |

## Issue To Mitigate

Incomplete or No Cache-control and Pragma HTTP Header Set

## Research

Cache-control is an HTTP header used to specify browser caching policies in both client requests and server responses. Policies include how a resource is cached, where it is cached and its maximum age before expiring. The cache-control header is broken up into directives, the most common directives:

***Cache-Control: max-age***

***Cache-Control: no-cache***

***Cache-Control: no-store***

***Cache-Control: public*** 

***Cache-Control: private***

***Cache-Control: must-revalidate***

The solution is to ensure the cache-control HTTP header is set with no-cache, no-store, must-revalidate; and that the pragma HTTP header is set with no-cache.

## To mitigate the Incomplete or No Cache-control and Pragma HTTP Header Set warning I added code to the startup class
In the Configuration method:
 services.AddResponseCaching((options) =>
            {              
                options.MaximumBodySize = 1024;
                options.UseCaseSensitivePaths = true;
            });

In the ConfigureServices method I added 2 methods:

app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (context) =>
                {
                    // Disable caching for all static files.
                    context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                    context.Context.Response.Headers["Pragma"] = "no-cache";
                    context.Context.Response.Headers["Expires"] = "-1";
                }

app.UseResponseCaching();
            app.Use(async (context, next) =>
            {
                if (context.Request.Method.Equals(System.Net.Http.HttpMethod.Get))
                {
                    context.Response.GetTypedHeaders().CacheControl =
                    new CacheControlHeaderValue()
                    {
                        Private = true,
                        MaxAge = TimeSpan.FromSeconds(10),
                        NoCache = true,
                        NoStore = true,
                        MustRevalidate = true
                        

                    };
                    context.Response.Headers[HeaderNames.Vary] =
                        new string[] { "Accept-Encoding" };
                }
                await next();
            });
            HttpResponseMessage response = new HttpResponseMessage();
            response.Headers.Pragma.ParseAdd("no-cache");

as per class lecture to mitigate the X-Cross headers I added the code supplied to the start up class :

  app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                await next();
            });

***Scan After Mitigation***

# ZAP Scanning Report

Generated on Thu, 18 Feb 2021 16:18:35


## Summary of Alerts

| Risk Level | Number of Alerts |
| --- | --- |
| High | 1 |
| Medium | 4 |
| Low | 17 |
| Informational | 10 |

## Alerts

| Name | Risk Level | Number of Instances |
| --- | --- | --- |
| Cross Site Scripting (Reflected) | High | 2 |
| Cross-Domain Misconfiguration | Medium | 4 |
| Vulnerable JS Library | Medium | 1 |
| X-Frame-Options Header Not Set | Medium | 731 |
| Cookie Without Secure Flag | Low | 2 |
| Cross-Domain JavaScript Source File Inclusion | Low | 2199 |
| Incomplete or No Cache-control and Pragma HTTP Header Set | Low | 775 |
| Server Leaks Information via "X-Powered-By" HTTP Response Header Field(s) | Low | 1098 |
| X-Content-Type-Options Header Missing | Low | 763 |
| Charset Mismatch  | Informational | 1 |
| Information Disclosure - Suspicious Comments | Informational | 4 |
| Loosely Scoped Cookie | Informational | 4 |
| Timestamp Disclosure - Unix | Informational | 441 |

## Alert Detail



  


### Cross Site Scripting (Reflected)
##### High (Low)

  


#### Description
<p>Cross-site Scripting (XSS) is an attack technique that involves echoing attacker-supplied code into a user's browser instance. A browser instance can be a standard web browser client, or a browser object embedded in a software product such as the browser within WinAmp, an RSS reader, or an email client. The code itself is usually written in HTML/JavaScript, but may also extend to VBScript, ActiveX, Java, Flash, or any other browser-supported technology.</p><p>When an attacker gets a user's browser to execute his/her code, the code will run within the security context (or zone) of the hosting web site. With this level of privilege, the code has the ability to read, modify and transmit any sensitive data accessible by the browser. A Cross-site Scripted user could have his/her account hijacked (cookie theft), their browser redirected to another location, or possibly shown fraudulent content delivered by the web site they are visiting. Cross-site Scripting attacks essentially compromise the trust relationship between a user and the web site. Applications utilizing browser object instances which load content from the file system may execute code under the local machine zone allowing for system compromise.</p><p></p><p>There are three types of Cross-site Scripting attacks: non-persistent, persistent and DOM-based.</p><p>Non-persistent attacks and DOM-based attacks require a user to either visit a specially crafted link laced with malicious code, or visit a malicious web page containing a web form, which when posted to the vulnerable site, will mount the attack. Using a malicious form will oftentimes take place when the vulnerable resource only accepts HTTP POST requests. In such a case, the form can be submitted automatically, without the victim's knowledge (e.g. by using JavaScript). Upon clicking on the malicious link or submitting the malicious form, the XSS payload will get echoed back and will get interpreted by the user's browser and execute. Another technique to send almost arbitrary requests (GET and POST) is by using an embedded client, such as Adobe Flash.</p><p>Persistent attacks occur when the malicious code is submitted to a web site where it's stored for a period of time. Examples of an attacker's favorite targets often include message board posts, web mail messages, and web chat software. The unsuspecting user is not required to interact with any additional site/link (e.g. an attacker site or a malicious link sent via email), just simply view the web page containing the code.</p>

  

* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D354](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D354)
  
  
  * Method: `POST`
  
  
  * Parameter: `RememberMe`
  
  
  * Attack: `<script>alert(1);</script>`
  
  
  * Evidence: `<script>alert(1);</script>`
  
  
  
* URL: [https://localhost:44365/Account/LogIn](https://localhost:44365/Account/LogIn)
  
  
  * Method: `POST`
  
  
  * Parameter: `RememberMe`
  
  
  * Attack: `<script>alert(1);</script>`
  
  
  * Evidence: `<script>alert(1);</script>`
  
  
  
  

Instances: 2

### Solution
<p>Phase: Architecture and Design</p><p>Use a vetted library or framework that does not allow this weakness to occur or provides constructs that make this weakness easier to avoid.</p><p>Examples of libraries and frameworks that make it easier to generate properly encoded output include Microsoft's Anti-XSS library, the OWASP ESAPI Encoding module, and Apache Wicket.</p><p></p><p>Phases: Implementation; Architecture and Design</p><p>Understand the context in which your data will be used and the encoding that will be expected. This is especially important when transmitting data between different components, or when generating outputs that can contain multiple encodings at the same time, such as web pages or multi-part mail messages. Study all expected communication protocols and data representations to determine the required encoding strategies.</p><p>For any data that will be output to another web page, especially any data that was received from external inputs, use the appropriate encoding on all non-alphanumeric characters.</p><p>Consult the XSS Prevention Cheat Sheet for more details on the types of encoding and escaping that are needed.</p><p></p><p>Phase: Architecture and Design</p><p>For any security checks that are performed on the client side, ensure that these checks are duplicated on the server side, in order to avoid CWE-602. Attackers can bypass the client-side checks by modifying values after the checks have been performed, or by changing the client to remove the client-side checks entirely. Then, these modified values would be submitted to the server.</p><p></p><p>If available, use structured mechanisms that automatically enforce the separation between data and code. These mechanisms may be able to provide the relevant quoting, encoding, and validation automatically, instead of relying on the developer to provide this capability at every point where output is generated.</p><p></p><p>Phase: Implementation</p><p>For every web page that is generated, use and specify a character encoding such as ISO-8859-1 or UTF-8. When an encoding is not specified, the web browser may choose a different encoding by guessing which encoding is actually being used by the web page. This can cause the web browser to treat certain sequences as special, opening up the client to subtle XSS attacks. See CWE-116 for more mitigations related to encoding/escaping.</p><p></p><p>To help mitigate XSS attacks against the user's session cookie, set the session cookie to be HttpOnly. In browsers that support the HttpOnly feature (such as more recent versions of Internet Explorer and Firefox), this attribute can prevent the user's session cookie from being accessible to malicious client-side scripts that use document.cookie. This is not a complete solution, since HttpOnly is not supported by all browsers. More importantly, XMLHTTPRequest and other powerful browser technologies provide read access to HTTP headers, including the Set-Cookie header in which the HttpOnly flag is set.</p><p></p><p>Assume all input is malicious. Use an "accept known good" input validation strategy, i.e., use an allow list of acceptable inputs that strictly conform to specifications. Reject any input that does not strictly conform to specifications, or transform it into something that does. Do not rely exclusively on looking for malicious or malformed inputs (i.e., do not rely on a deny list). However, deny lists can be useful for detecting potential attacks or determining which inputs are so malformed that they should be rejected outright.</p><p></p><p>When performing input validation, consider all potentially relevant properties, including length, type of input, the full range of acceptable values, missing or extra inputs, syntax, consistency across related fields, and conformance to business rules. As an example of business rule logic, "boat" may be syntactically valid because it only contains alphanumeric characters, but it is not valid if you are expecting colors such as "red" or "blue."</p><p></p><p>Ensure that you perform input validation at well-defined interfaces within the application. This will help protect the application even if a component is reused or moved elsewhere.</p>

### Other information
<p>Raised with LOW confidence as the Content-Type is not HTML</p>

### Reference
* http://projects.webappsec.org/Cross-Site-Scripting
* http://cwe.mitre.org/data/definitions/79.html

  
#### CWE Id : 79

#### WASC Id : 8

#### Source ID : 1

  

  

### Cross-Domain Misconfiguration
##### Medium (Medium)

  


#### Description
<p>Web browser data loading may be possible, due to a Cross Origin Resource Sharing (CORS) misconfiguration on the web server</p>

  

* URL: [https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js](https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `Access-Control-Allow-Origin: *`
  
  
  
* URL: [https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js](https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `Access-Control-Allow-Origin: *`
  
  
  
* URL: [https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js](https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `Access-Control-Allow-Origin: *`
  
  
  
  

Instances: 3

### Solution
<p>Ensure that sensitive data is not available in an unauthenticated manner (using IP address white-listing, for instance).</p><p>Configure the "Access-Control-Allow-Origin" HTTP header to a more restrictive set of domains, or remove all CORS headers entirely, to allow the web browser to enforce the Same Origin Policy (SOP) in a more restrictive manner.</p>

### Other information
<p>The CORS misconfiguration on the web server permits cross-domain read requests from arbitrary third party domains, using unauthenticated APIs on this domain. Web browser implementations do not permit arbitrary third parties to read the response from authenticated APIs, however. This reduces the risk somewhat. This misconfiguration could be used by an attacker to access data that is available in an unauthenticated manner, but which uses some other form of security, such as IP address white-listing.</p>

### Reference
* http://www.hpenterprisesecurity.com/vulncat/en/vulncat/vb/html5_overly_permissive_cors_policy.html

  
#### CWE Id : 264

#### WASC Id : 14

#### Source ID : 3

  

  

### Cross-Domain Misconfiguration
##### Medium (Medium)

  


#### Description
<p>Web browser data loading may be possible, due to a Cross Origin Resource Sharing (CORS) misconfiguration on the web server</p>

  

* URL: [https://location.services.mozilla.com/v1/country?key=7e40f68c-7938-4c5d-9f95-e61647c213eb](https://location.services.mozilla.com/v1/country?key=7e40f68c-7938-4c5d-9f95-e61647c213eb)
  
  
  * Method: `GET`
  
  
  * Evidence: `Access-Control-Allow-Origin: *`
  
  
  
  

Instances: 1

### Solution
<p>Ensure that sensitive data is not available in an unauthenticated manner (using IP address white-listing, for instance).</p><p>Configure the "Access-Control-Allow-Origin" HTTP header to a more restrictive set of domains, or remove all CORS headers entirely, to allow the web browser to enforce the Same Origin Policy (SOP) in a more restrictive manner.</p>

### Other information
<p>The CORS misconfiguration on the web server permits cross-domain read requests from arbitrary third party domains, using unauthenticated APIs on this domain. Web browser implementations do not permit arbitrary third parties to read the response from authenticated APIs, however. This reduces the risk somewhat. This misconfiguration could be used by an attacker to access data that is available in an unauthenticated manner, but which uses some other form of security, such as IP address white-listing.</p>

### Reference
* http://www.hpenterprisesecurity.com/vulncat/en/vulncat/vb/html5_overly_permissive_cors_policy.html

  
#### CWE Id : 264

#### WASC Id : 14

#### Source ID : 3

  

  

### Vulnerable JS Library
##### Medium (Medium)

  


#### Description
<p>The identified library jquery, version 3.4.1 is vulnerable.</p>

  

* URL: [https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js](https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `/3.4.1/jquery.min.js`
  
  
  
  

Instances: 1

### Solution
<p>Please upgrade to the latest version of jquery.</p>

### Other information
<p>CVE-2020-11023</p><p>CVE-2020-11022</p><p></p>

### Reference
* https://blog.jquery.com/2020/04/10/jquery-3-5-0-released/
* 

  
#### CWE Id : 829

#### Source ID : 3

  

  

### X-Frame-Options Header Not Set
##### Medium (Medium)

  


#### Description
<p>X-Frame-Options header is not included in the HTTP response to protect against 'ClickJacking' attacks.</p>

  

* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D54](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D54)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FUser](https://localhost:44365/Account/LogIn?returnUrl=%2FUser)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D134](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D134)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D347](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D347)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D214](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D214)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D267](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D267)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D53](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D53)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D215](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D215)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D348](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D348)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D268](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D268)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D135](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D135)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D49](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D49)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D52](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D52)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D212](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D212)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D269](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D269)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D345](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D345)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D136](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D136)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D48](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D48)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D51](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D51)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D346](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D346)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  

Instances: 731

### Solution
<p>Most modern Web browsers support the X-Frame-Options HTTP header. Ensure it's set on all web pages returned by your site (if you expect the page to be framed only by pages on your server (e.g. it's part of a FRAMESET) then you'll want to use SAMEORIGIN, otherwise if you never expect the page to be framed, you should use DENY. Alternatively consider implementing Content Security Policy's "frame-ancestors" directive. </p>

### Reference
* https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options

  
#### CWE Id : 16

#### WASC Id : 15

#### Source ID : 3

  

  

### Cookie Without Secure Flag
##### Low (Medium)

  


#### Description
<p>A cookie has been set without the secure flag, which means that the cookie can be accessed via unencrypted connections.</p>

  

* URL: [https://localhost:44365/Account/LogIn](https://localhost:44365/Account/LogIn)
  
  
  * Method: `GET`
  
  
  * Parameter: `.AspNetCore.Antiforgery.JVpPmIrw7FY`
  
  
  * Evidence: `Set-Cookie: .AspNetCore.Antiforgery.JVpPmIrw7FY`
  
  
  
* URL: [https://localhost:44365/Account/Register](https://localhost:44365/Account/Register)
  
  
  * Method: `GET`
  
  
  * Parameter: `.AspNetCore.Antiforgery.JVpPmIrw7FY`
  
  
  * Evidence: `Set-Cookie: .AspNetCore.Antiforgery.JVpPmIrw7FY`
  
  
  
  

Instances: 2

### Solution
<p>Whenever a cookie contains sensitive information or is a session token, then it should always be passed using an encrypted channel. Ensure that the secure flag is set for cookies containing such sensitive information.</p>

### Reference
* https://owasp.org/www-project-web-security-testing-guide/v41/4-Web_Application_Security_Testing/06-Session_Management_Testing/02-Testing_for_Cookies_Attributes.html

  
#### CWE Id : 614

#### WASC Id : 13

#### Source ID : 3

  

  

### Cross-Domain JavaScript Source File Inclusion
##### Low (Medium)

  


#### Description
<p>The page includes one or more script files from a third-party domain.</p>

  

* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D336](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D336)
  
  
  * Method: `GET`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D233](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D233)
  
  
  * Method: `POST`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D100](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D100)
  
  
  * Method: `POST`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D203](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D203)
  
  
  * Method: `GET`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D270](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D270)
  
  
  * Method: `POST`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D122](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D122)
  
  
  * Method: `POST`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D255](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D255)
  
  
  * Method: `POST`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D18](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D18)
  
  
  * Method: `GET`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D55](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D55)
  
  
  * Method: `GET`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D314](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D314)
  
  
  * Method: `GET`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D54](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D54)
  
  
  * Method: `GET`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D350](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D350)
  
  
  * Method: `GET`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D20](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D20)
  
  
  * Method: `POST`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D43](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D43)
  
  
  * Method: `POST`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D204](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D204)
  
  
  * Method: `GET`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D337](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D337)
  
  
  * Method: `GET`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D39](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D39)
  
  
  * Method: `GET`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D234](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D234)
  
  
  * Method: `POST`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D101](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D101)
  
  
  * Method: `POST`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D254](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D254)
  
  
  * Method: `POST`
  
  
  * Parameter: `https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js`
  
  
  * Evidence: `<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js"></script>`
  
  
  
  

Instances: 2199

### Solution
<p>Ensure JavaScript source files are loaded from only trusted sources, and the sources can't be controlled by end users of the application.</p>

### Reference
* 

  
#### CWE Id : 829

#### WASC Id : 15

#### Source ID : 3

  

  

### Incomplete or No Cache-control and Pragma HTTP Header Set
##### Low (Medium)

  


#### Description
<p>The cache-control and pragma HTTP header have not been set properly or are missing allowing the browser and proxies to cache content.</p>

  

* URL: [https://aus5.mozilla.org/update/3/SystemAddons/84.0.2/20210105180113/WINNT_x86_64-msvc-x64/en-US/release/Windows_NT%2010.0.0.0.19041.804%20(x64)/default/default/update.xml](https://aus5.mozilla.org/update/3/SystemAddons/84.0.2/20210105180113/WINNT_x86_64-msvc-x64/en-US/release/Windows_NT%2010.0.0.0.19041.804%20(x64)/default/default/update.xml)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `public, max-age=90`
  
  
  
  

Instances: 1

### Solution
<p>Whenever possible ensure the cache-control HTTP header is set with no-cache, no-store, must-revalidate; and that the pragma HTTP header is set with no-cache.</p>

### Reference
* https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching

  
#### CWE Id : 525

#### WASC Id : 13

#### Source ID : 3

  

  

### Incomplete or No Cache-control and Pragma HTTP Header Set
##### Low (Medium)

  


#### Description
<p>The cache-control and pragma HTTP header have not been set properly or are missing allowing the browser and proxies to cache content.</p>

  

* URL: [https://snippets.cdn.mozilla.net/us-west/bundles-pregen/Firefox/en-us/default.json](https://snippets.cdn.mozilla.net/us-west/bundles-pregen/Firefox/en-us/default.json)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `max-age=600`
  
  
  
  

Instances: 1

### Solution
<p>Whenever possible ensure the cache-control HTTP header is set with no-cache, no-store, must-revalidate; and that the pragma HTTP header is set with no-cache.</p>

### Reference
* https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching

  
#### CWE Id : 525

#### WASC Id : 13

#### Source ID : 3

  

  

### Incomplete or No Cache-control and Pragma HTTP Header Set
##### Low (Medium)

  


#### Description
<p>The cache-control and pragma HTTP header have not been set properly or are missing allowing the browser and proxies to cache content.</p>

  

* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  
  

Instances: 1

### Solution
<p>Whenever possible ensure the cache-control HTTP header is set with no-cache, no-store, must-revalidate; and that the pragma HTTP header is set with no-cache.</p>

### Reference
* https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching

  
#### CWE Id : 525

#### WASC Id : 13

#### Source ID : 3

  

  

### Incomplete or No Cache-control and Pragma HTTP Header Set
##### Low (Medium)

  


#### Description
<p>The cache-control and pragma HTTP header have not been set properly or are missing allowing the browser and proxies to cache content.</p>

  

* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Parameter: `Pragma`
  
  
  * Evidence: `cache`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `s-maxage=900, public`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `s-maxage=900,public`
  
  
  
  

Instances: 3

### Solution
<p>Whenever possible ensure the cache-control HTTP header is set with no-cache, no-store, must-revalidate; and that the pragma HTTP header is set with no-cache.</p>

### Reference
* https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching

  
#### CWE Id : 525

#### WASC Id : 13

#### Source ID : 3

  

  

### Incomplete or No Cache-control and Pragma HTTP Header Set
##### Low (Medium)

  


#### Description
<p>The cache-control and pragma HTTP header have not been set properly or are missing allowing the browser and proxies to cache content.</p>

  

* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D213](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D213)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D346](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D346)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D345](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D345)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D212](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D212)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FForum](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FForum)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D215](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D215)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D348](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D348)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D347](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D347)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D214](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D214)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D217](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D217)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D216](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D216)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D349](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D349)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/REFERENCES/ONLINE](https://localhost:44365/REFERENCES/ONLINE)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D219](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D219)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D80](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D80)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D218](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D218)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D250](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D250)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D251](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D251)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D252](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D252)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D80](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D80)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  

Instances: 735

### Solution
<p>Whenever possible ensure the cache-control HTTP header is set with no-cache, no-store, must-revalidate; and that the pragma HTTP header is set with no-cache.</p>

### Reference
* https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching

  
#### CWE Id : 525

#### WASC Id : 13

#### Source ID : 3

  

  

### Incomplete or No Cache-control and Pragma HTTP Header Set
##### Low (Medium)

  


#### Description
<p>The cache-control and pragma HTTP header have not been set properly or are missing allowing the browser and proxies to cache content.</p>

  

* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/normandy-recipes-capabilities/changeset?_expected=1613518332852](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/normandy-recipes-capabilities/changeset?_expected=1613518332852)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/search-config/changeset?_expected=1613587855073&_since=%221605203142486%22](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/search-config/changeset?_expected=1613587855073&_since=%221605203142486%22)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=message-groups&bucket=main](https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=message-groups&bucket=main)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `max-age=60`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/whats-new-panel/changeset?_expected=1611670765047](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/whats-new-panel/changeset?_expected=1611670765047)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/pioneer-study-addons-v1/changeset?_expected=1607042143590](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/pioneer-study-addons-v1/changeset?_expected=1607042143590)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=partitioning-exempt-urls&bucket=main](https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=partitioning-exempt-urls&bucket=main)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `max-age=60`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/public-suffix-list/changeset?_expected=1575468539758](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/public-suffix-list/changeset?_expected=1575468539758)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/pinning/collections/pins/changeset?_expected=1485794868067](https://firefox.settings.services.mozilla.com/v1/buckets/pinning/collections/pins/changeset?_expected=1485794868067)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=fxmonitor-breaches&bucket=main](https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=fxmonitor-breaches&bucket=main)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `max-age=60`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/hijack-blocklists?_expected=1605801189258](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/hijack-blocklists?_expected=1605801189258)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=cfr-fxa&bucket=main](https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=cfr-fxa&bucket=main)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `max-age=60`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/tippytop/changeset?_expected=1603216320139](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/tippytop/changeset?_expected=1603216320139)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/top-sites/changeset?_expected=1611838808382&_since=%221608239138396%22](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/top-sites/changeset?_expected=1611838808382&_since=%221608239138396%22)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/url-classifier-skip-urls/changeset?_expected=1606870304609](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/url-classifier-skip-urls/changeset?_expected=1606870304609)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/plugins?_expected=1603126502200](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/plugins?_expected=1603126502200)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/search-default-override-allowlist?_expected=1595254618540](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/search-default-override-allowlist?_expected=1595254618540)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/security-state/collections/onecrl/changeset?_expected=1612908330359&_since=%221607546180239%22](https://firefox.settings.services.mozilla.com/v1/buckets/security-state/collections/onecrl/changeset?_expected=1612908330359&_since=%221607546180239%22)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/password-recipes?_expected=1600889167888](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/password-recipes?_expected=1600889167888)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/anti-tracking-url-decoration?_expected=1564511755134](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/anti-tracking-url-decoration?_expected=1564511755134)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  

Instances: 34

### Solution
<p>Whenever possible ensure the cache-control HTTP header is set with no-cache, no-store, must-revalidate; and that the pragma HTTP header is set with no-cache.</p>

### Reference
* https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching

  
#### CWE Id : 525

#### WASC Id : 13

#### Source ID : 3

  

  

### Server Leaks Information via "X-Powered-By" HTTP Response Header Field(s)
##### Low (Medium)

  


#### Description
<p>The web/application server is leaking information via one or more "X-Powered-By" HTTP response headers. Access to such information may facilitate attackers identifying other frameworks/components your web application is reliant upon and the vulnerabilities such components may be subject to.</p>

  

* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D63](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D63)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D3](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D3)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D4](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D4)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D64](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D64)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D2](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D2)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D189](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D189)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D5](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D5)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D65](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D65)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D187](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D187)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D280](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D280)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D5](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D5)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D188](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D188)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D6](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D6)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D66](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D66)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D186](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D186)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D281](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D281)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D7](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D7)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D4](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D4)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D67](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D67)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D185](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D185)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  

Instances: 1098

### Solution
<p>Ensure that your web server, application server, load balancer, etc. is configured to suppress "X-Powered-By" headers.</p>

### Reference
* http://blogs.msdn.com/b/varunm/archive/2013/04/23/remove-unwanted-http-response-headers.aspx
* http://www.troyhunt.com/2012/02/shhh-dont-let-your-response-headers.html

  
#### CWE Id : 200

#### WASC Id : 13

#### Source ID : 3

  

  

### X-Content-Type-Options Header Missing
##### Low (Medium)

  


#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>

  

* URL: [https://ftp.mozilla.org/pub/system-addons/reset-search-defaults/reset-search-defaults@mozilla.com-1.0.3-signed.xpi](https://ftp.mozilla.org/pub/system-addons/reset-search-defaults/reset-search-defaults@mozilla.com-1.0.3-signed.xpi)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  

Instances: 1

### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>

### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>

### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16

#### WASC Id : 15

#### Source ID : 3

  

  

### X-Content-Type-Options Header Missing
##### Low (Medium)

  


#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>

  

* URL: [https://snippets.cdn.mozilla.net/us-west/bundles-pregen/Firefox/en-us/default.json](https://snippets.cdn.mozilla.net/us-west/bundles-pregen/Firefox/en-us/default.json)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  

Instances: 1

### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>

### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>

### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16

#### WASC Id : 15

#### Source ID : 3

  

  

### X-Content-Type-Options Header Missing
##### Low (Medium)

  


#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>

  

* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  

Instances: 1

### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>

### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>

### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16

#### WASC Id : 15

#### Source ID : 3

  

  

### X-Content-Type-Options Header Missing
##### Low (Medium)

  


#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>

  

* URL: [https://tracking-protection.cdn.mozilla.net/analytics-track-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/analytics-track-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/block-flash-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/block-flash-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/mozstd-trackwhite-digest256/84.0/1611615822](https://tracking-protection.cdn.mozilla.net/mozstd-trackwhite-digest256/84.0/1611615822)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/base-fingerprinting-track-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/base-fingerprinting-track-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/except-flashallow-digest256/1490633678](https://tracking-protection.cdn.mozilla.net/except-flashallow-digest256/1490633678)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/except-flashsubdoc-digest256/1517935265](https://tracking-protection.cdn.mozilla.net/except-flashsubdoc-digest256/1517935265)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/google-trackwhite-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/google-trackwhite-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/base-cryptomining-track-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/base-cryptomining-track-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/block-flashsubdoc-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/block-flashsubdoc-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-tracking-protection-twitter-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/social-tracking-protection-twitter-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-tracking-protection-linkedin-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/social-tracking-protection-linkedin-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/content-track-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/content-track-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/except-flash-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/except-flash-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-tracking-protection-facebook-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/social-tracking-protection-facebook-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/ads-track-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/ads-track-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/allow-flashallow-digest256/1490633678](https://tracking-protection.cdn.mozilla.net/allow-flashallow-digest256/1490633678)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-track-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/social-track-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  

Instances: 17

### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>

### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>

### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16

#### WASC Id : 15

#### Source ID : 3

  

  

### X-Content-Type-Options Header Missing
##### Low (Medium)

  


#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>

  

* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  

Instances: 1

### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>

### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>

### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16

#### WASC Id : 15

#### Source ID : 3

  

  

### X-Content-Type-Options Header Missing
##### Low (Medium)

  


#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>

  

* URL: [https://content-signature-2.cdn.mozilla.net/chains/remote-settings.content-signature.mozilla.org-2021-03-22-17-58-04.chain](https://content-signature-2.cdn.mozilla.net/chains/remote-settings.content-signature.mozilla.org-2021-03-22-17-58-04.chain)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://content-signature-2.cdn.mozilla.net/chains/pinning-preload.content-signature.mozilla.org-2021-03-22-17-58-04.chain](https://content-signature-2.cdn.mozilla.net/chains/pinning-preload.content-signature.mozilla.org-2021-03-22-17-58-04.chain)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://content-signature-2.cdn.mozilla.net/chains/onecrl.content-signature.mozilla.org-2021-03-22-17-58-02.chain](https://content-signature-2.cdn.mozilla.net/chains/onecrl.content-signature.mozilla.org-2021-03-22-17-58-02.chain)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  

Instances: 3

### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>

### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>

### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16

#### WASC Id : 15

#### Source ID : 3

  

  

### X-Content-Type-Options Header Missing
##### Low (Medium)

  


#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>

  

* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D344](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D344)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FForum](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FForum)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D211](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D211)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D212](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D212)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D345](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D345)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D346](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D346)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D213](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D213)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D119](https://localhost:44365/Account/Login?ReturnUrl=%2FHome%2FComment%3FpostId%3D119)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D214](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D214)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D347](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D347)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D340](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D340)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D341](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D341)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D342](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D342)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D210](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D210)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D343](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D343)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D219](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D219)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/REFERENCES/PRINTED](https://localhost:44365/REFERENCES/PRINTED)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D87](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D87)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D88](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D88)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
* URL: [https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D89](https://localhost:44365/Account/LogIn?returnUrl=%2FHome%2FComment%3FpostId%3D89)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  

Instances: 738

### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>

### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>

### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16

#### WASC Id : 15

#### Source ID : 3

  

  

### X-Content-Type-Options Header Missing
##### Low (Medium)

  


#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>

  

* URL: [https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=84.0&pver=2.2](https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=84.0&pver=2.2)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  

Instances: 1

### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>

### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>

### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16

#### WASC Id : 15

#### Source ID : 3

  

  

### Charset Mismatch 
##### Informational (Low)

  


#### Description
<p>This check identifies responses where the HTTP Content-Type header declares a charset different from the charset defined by the body of the HTML or XML. When there's a charset mismatch between the HTTP header and content body Web browsers can be forced into an undesirable content-sniffing mode to determine the content's correct character set.</p><p></p><p>An attacker could manipulate content on the page to be interpreted in an encoding of their choice. For example, if an attacker can control content at the beginning of the page, they could inject script using UTF-7 encoded text and manipulate some browsers into interpreting that text.</p>

  

* URL: [https://aus5.mozilla.org/update/3/SystemAddons/84.0.2/20210105180113/WINNT_x86_64-msvc-x64/en-US/release/Windows_NT%2010.0.0.0.19041.804%20(x64)/default/default/update.xml](https://aus5.mozilla.org/update/3/SystemAddons/84.0.2/20210105180113/WINNT_x86_64-msvc-x64/en-US/release/Windows_NT%2010.0.0.0.19041.804%20(x64)/default/default/update.xml)
  
  
  * Method: `GET`
  
  
  
  

Instances: 1

### Solution
<p>Force UTF-8 for all text content in both the HTTP header and meta tags in HTML or encoding declarations in XML.</p>

### Other information
<p>There was a charset mismatch between the HTTP Header and the XML encoding declaration: [utf-8] and [null] do not match.</p>

### Reference
* http://code.google.com/p/browsersec/wiki/Part2#Character_set_handling_and_detection

  
#### CWE Id : 16

#### WASC Id : 15

#### Source ID : 3

  

  

### Information Disclosure - Suspicious Comments
##### Informational (Low)

  


#### Description
<p>The response appears to contain suspicious comments which may help an attacker. Note: Matches made within script blocks or files are against the entire content not only comments.</p>

  

* URL: [https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js](https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `username`
  
  
  
* URL: [https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js](https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `select`
  
  
  
  

Instances: 2

### Solution
<p>Remove all comments that return information that may help an attacker and fix any underlying problems they refer to.</p>

### Other information
<p>The following pattern was used: \bUSERNAME\b and was detected in the element starting with: "!function(e,t){"use strict";"object"==typeof module&&"object"==typeof module.exports?module.exports=e.document?t(e,!0):function(", see evidence field for the suspicious comment/snippet.</p>

### Reference
* 

  
#### CWE Id : 200

#### WASC Id : 13

#### Source ID : 3

  

  

### Information Disclosure - Suspicious Comments
##### Informational (Low)

  


#### Description
<p>The response appears to contain suspicious comments which may help an attacker. Note: Matches made within script blocks or files are against the entire content not only comments.</p>

  

* URL: [https://localhost:44365/lib/bootstrap/dist/js/bootstrap.bundle.min.js](https://localhost:44365/lib/bootstrap/dist/js/bootstrap.bundle.min.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `from`
  
  
  
* URL: [https://localhost:44365/lib/jquery/dist/jquery.min.js](https://localhost:44365/lib/jquery/dist/jquery.min.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `username`
  
  
  
  

Instances: 2

### Solution
<p>Remove all comments that return information that may help an attacker and fix any underlying problems they refer to.</p>

### Other information
<p>The following pattern was used: \bFROM\b and was detected in the element starting with: "!function(t,e){"object"==typeof exports&&"undefined"!=typeof module?e(exports,require("jquery")):"function"==typeof define&&defi", see evidence field for the suspicious comment/snippet.</p>

### Reference
* 

  
#### CWE Id : 200

#### WASC Id : 13

#### Source ID : 3

  

  

### Loosely Scoped Cookie
##### Informational (Low)

  


#### Description
<p>Cookies can be scoped by domain or path. This check is only concerned with domain scope.The domain scope applied to a cookie determines which domains can access it. For example, a cookie can be scoped strictly to a subdomain e.g. www.nottrusted.com, or loosely scoped to a parent domain e.g. nottrusted.com. In the latter case, any subdomain of nottrusted.com can access the cookie. Loosely scoped cookies are common in mega-applications like google.com and live.com. Cookies set from a subdomain like app.foo.bar are transmitted only to that domain by the browser. However, cookies scoped to a parent-level domain may be transmitted to the parent, or any subdomain of the parent.</p>

  

* URL: [https://localhost:44365/Account/LogIn](https://localhost:44365/Account/LogIn)
  
  
  * Method: `GET`
  
  
  
* URL: [https://localhost:44365/Account/Register](https://localhost:44365/Account/Register)
  
  
  * Method: `GET`
  
  
  
* URL: [https://localhost:44365/Account/LogIn](https://localhost:44365/Account/LogIn)
  
  
  * Method: `GET`
  
  
  
* URL: [https://localhost:44365/Account/LogIn](https://localhost:44365/Account/LogIn)
  
  
  * Method: `POST`
  
  
  
  

Instances: 4

### Solution
<p>Always scope cookies to a FQDN (Fully Qualified Domain Name).</p>

### Other information
<p>The origin domain used for comparison was: </p><p>localhost</p><p>.AspNetCore.Antiforgery.JVpPmIrw7FY=CfDJ8JigQVXMJtVNqUeJRVxaAJs-zrUTiyySpM-_ie7-0Joaeh1xjKW3KNdMxnCnrNnu7XVbO_HOwTgoIcqe_QloQNVvcSnnqLQ6Gqqb9TdfBDw5ebifediNg3tmV7ISIJAch-gzreM1tmFrtqdlyd20c3U</p><p></p>

### Reference
* https://tools.ietf.org/html/rfc6265#section-4.1
* https://owasp.org/www-project-web-security-testing-guide/v41/4-Web_Application_Security_Testing/06-Session_Management_Testing/02-Testing_for_Cookies_Attributes.html
* http://code.google.com/p/browsersec/wiki/Part2#Same-origin_policy_for_cookies

  
#### CWE Id : 565

#### WASC Id : 15

#### Source ID : 3

  

  

### Timestamp Disclosure - Unix
##### Informational (Low)

  


#### Description
<p>A timestamp was disclosed by the application/web server - Unix</p>

  

* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `97123117`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15050790`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15010262`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15078719`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22788054`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22657590`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22657784`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15050987`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15034676`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22716421`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22657593`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22657873`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15077893`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22657251`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22788057`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15051038`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22654527`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22491016`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22793842`
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22734312`
  
  
  
  

Instances: 90

### Solution
<p>Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.</p>

### Other information
<p>97123117, which evaluates to: 1973-01-28 18:38:37</p>

### Reference
* http://projects.webappsec.org/w/page/13246936/Information%20Leakage

  
#### CWE Id : 200

#### WASC Id : 13

#### Source ID : 3

  

  

### Timestamp Disclosure - Unix
##### Informational (Low)

  


#### Description
<p>A timestamp was disclosed by the application/web server - Unix</p>

  

* URL: [https://tracking-protection.cdn.mozilla.net/google-trackwhite-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/google-trackwhite-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Evidence: `1608188613`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/block-flashsubdoc-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/block-flashsubdoc-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Evidence: `1604686195`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/base-cryptomining-track-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/base-cryptomining-track-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Evidence: `1608188613`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-tracking-protection-twitter-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/social-tracking-protection-twitter-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Evidence: `1608188613`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-track-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/social-track-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Evidence: `1608188613`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-tracking-protection-facebook-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/social-tracking-protection-facebook-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Evidence: `1608188613`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/allow-flashallow-digest256/1490633678](https://tracking-protection.cdn.mozilla.net/allow-flashallow-digest256/1490633678)
  
  
  * Method: `GET`
  
  
  * Evidence: `1490633678`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/except-flashallow-digest256/1490633678](https://tracking-protection.cdn.mozilla.net/except-flashallow-digest256/1490633678)
  
  
  * Method: `GET`
  
  
  * Evidence: `1490633678`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/mozstd-trackwhite-digest256/84.0/1611615822](https://tracking-protection.cdn.mozilla.net/mozstd-trackwhite-digest256/84.0/1611615822)
  
  
  * Method: `GET`
  
  
  * Evidence: `1611615822`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/except-flash-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/except-flash-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Evidence: `1604686195`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/except-flashsubdoc-digest256/1517935265](https://tracking-protection.cdn.mozilla.net/except-flashsubdoc-digest256/1517935265)
  
  
  * Method: `GET`
  
  
  * Evidence: `1517935265`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/base-fingerprinting-track-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/base-fingerprinting-track-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Evidence: `1608188613`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/ads-track-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/ads-track-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Evidence: `1608188613`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-tracking-protection-linkedin-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/social-tracking-protection-linkedin-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Evidence: `1608188613`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/block-flash-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/block-flash-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Evidence: `1604686195`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/analytics-track-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/analytics-track-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Evidence: `1608188613`
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/content-track-digest256/84.0/1608188613](https://tracking-protection.cdn.mozilla.net/content-track-digest256/84.0/1608188613)
  
  
  * Method: `GET`
  
  
  * Evidence: `1608188613`
  
  
  
  

Instances: 17

### Solution
<p>Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.</p>

### Other information
<p>1608188613, which evaluates to: 2020-12-16 23:03:33</p>

### Reference
* http://projects.webappsec.org/w/page/13246936/Information%20Leakage

  
#### CWE Id : 200

#### WASC Id : 13

#### Source ID : 3

  

  

### Timestamp Disclosure - Unix
##### Informational (Low)

  


#### Description
<p>A timestamp was disclosed by the application/web server - Unix</p>

  

* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613472601`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1612980748`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `20210209`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613156426`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613214010`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613152800`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613390443`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613033976`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613401200`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `968126156`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613382866`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `104909267`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613134800`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613518721`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613001600`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1612936800`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613383200`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613048415`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `521366740`
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `2147483647`
  
  
  
  

Instances: 20

### Solution
<p>Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.</p>

### Other information
<p>1613472601, which evaluates to: 2021-02-16 02:50:01</p>

### Reference
* http://projects.webappsec.org/w/page/13246936/Information%20Leakage

  
#### CWE Id : 200

#### WASC Id : 13

#### Source ID : 3

  

  

### Timestamp Disclosure - Unix
##### Informational (Low)

  


#### Description
<p>A timestamp was disclosed by the application/web server - Unix</p>

  

* URL: [https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js](https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `2147483647`
  
  
  
  

Instances: 1

### Solution
<p>Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.</p>

### Other information
<p>2147483647, which evaluates to: 2038-01-18 19:14:07</p>

### Reference
* http://projects.webappsec.org/w/page/13246936/Information%20Leakage

  
#### CWE Id : 200

#### WASC Id : 13

#### Source ID : 3

  

  

### Timestamp Disclosure - Unix
##### Informational (Low)

  


#### Description
<p>A timestamp was disclosed by the application/web server - Unix</p>

  

* URL: [https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=84.0&pver=2.2](https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=84.0&pver=2.2)
  
  
  * Method: `POST`
  
  
  * Evidence: `1490633678`
  
  
  
* URL: [https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=84.0&pver=2.2](https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=84.0&pver=2.2)
  
  
  * Method: `POST`
  
  
  * Evidence: `1517935265`
  
  
  
* URL: [https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=84.0&pver=2.2](https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=84.0&pver=2.2)
  
  
  * Method: `POST`
  
  
  * Evidence: `1608188613`
  
  
  
* URL: [https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=84.0&pver=2.2](https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=84.0&pver=2.2)
  
  
  * Method: `POST`
  
  
  * Evidence: `1611615822`
  
  
  
* URL: [https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=84.0&pver=2.2](https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=84.0&pver=2.2)
  
  
  * Method: `POST`
  
  
  * Evidence: `1604686195`
  
  
  
  

Instances: 5

### Solution
<p>Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.</p>

### Other information
<p>1490633678, which evaluates to: 2017-03-27 09:54:38</p>

### Reference
* http://projects.webappsec.org/w/page/13246936/Information%20Leakage

  
#### CWE Id : 200

#### WASC Id : 13

#### Source ID : 3

  

  

### Timestamp Disclosure - Unix
##### Informational (Low)

  


#### Description
<p>A timestamp was disclosed by the application/web server - Unix</p>

  

* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `410661158`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `1571243437`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `11657763`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `2133023695`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `328630297`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `29020808`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `2019953811`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `1840032839`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `1966905954`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `503320555`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `70196972`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `1906908547`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `1171953868`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `78944020`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `845894403`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `759849205`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `1598706624`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613587101965&_since=%221608230359579%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `228273241`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `48881308`
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `40767652`
  
  
  
  

Instances: 308

### Solution
<p>Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.</p>

### Other information
<p>410661158, which evaluates to: 1983-01-05 16:32:38</p>

### Reference
* http://projects.webappsec.org/w/page/13246936/Information%20Leakage

  
#### CWE Id : 200

#### WASC Id : 13

#### Source ID : 3


