Google reCAPTCHA v2.0 for ASP.Net by Alexandre Pigeot

1. Usage

	This library contains a simple method called ReCaptcha.Verify() that returns true if Google's reCAPTCHA is verified. For the beginners:
	
	   1 - Wrap your form's code in an if statement and call the Verify method with your secret key in the parameter (response and remoteip are automatically fetched from your user and posted for you).
	   
		   if ( ReCaptcha.Verify("yoursecret") ) {
		   
			   // Your form code comes here
			   
		   }
		   
	   2 - There is no 2.

2. Disclaimer

	This package does not aim to provide a serious fool-proof framework nor does it return the error-codes provided by Google. Maybe these features will be available in a future version, but for now it is simply a little piece of code that I was tired of rewriting for every web app I developp and I thought other web designers might enjoy the convenience. If you have the time, keep looking for an official version from either Google (yeh, google luck with that) or Microsoft.
	
	Thanks to Newtonsoft for their ground-breaking, award-winning and better-than-the-original Json libraries. You guys have helped build the ASP.Net that we love.
	
	I do not own any of the brand names mentioned in this document. I have used the excellent Microsoft Visual Studio Community 2013 and ASP.Net 4.5.1 (while learning the exciting new release).
	
3. Support

	No support provided under any circumstances. If you need help with anything, you can write me a nice email addressed to alexandrepigeot [at] gmail [dot] com with "ReCaptcha" in the subject. I might or might not reply. Depends. Also, if you have done a better job than me with a similar project, thanks to let me know so I can retract this publication. I hate spamming official services with unofficial projects but this time I really didn't find any other option.
	
Keep on keeping on!!

Alex