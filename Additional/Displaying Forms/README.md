# Displaying a Form in an XProcess.

You may be asking, displaying forms? Why would that be difficult?

The problem is, for a Form control to be displayed in an environment that does not have a static instance of ```System.Windows.Application``` instatiated can cause quite a lot of issues. You can either trust me on this one, or go and read up on why this is such a tricky situation to get around with.

