# SPACats WebAssembly

This project is based on the [Cofoundry SPA example application](https://github.com/cofoundry-cms/Cofoundry.Samples.SPASite), ported from VueJs into Blazor. This variant of the app uses the WebAssembly client-side rendering mode and minimal APIs for data access.

If you want to run the project, follow the instructions on the [original Cofoundry sample repository](https://github.com/cofoundry-cms/Cofoundry.Samples.SPASite) to configure Cofoundry and initialize the data.

> Note that these applications are experiments and are not necessarily production ready code. Shortcuts or hacks may have been made to further the learning or experimentation process. 

## Points of note:

- Pre-rendering is problematic with initializing state and seems unnecessary for this rendering mode and so has been disabled. I can't expect you'd ever use web assembly for the kind of website that requires SSR.
- Adding custom errors to `EditContext` is a bit convoluted compared to typical ASP.NET `ModelState`, and the validation controls don't let you have a "catch all" validation summary to show errors not displayed in inline field validators, which is a feature you also get in MVC.
- I've copied data model code from the server/cofoundry app into the client app rather than reference a shared project. This seems to be what MS do in sample code, and after giving it come consideration it seems to me that maintaining a shared project of models would also be awkward, but I guess it depends on the structure of your solution e.g. if you have dedicated API dtos then it would make more sense. At least with duplicated models you can tailor them exactly to the client app requirements.
