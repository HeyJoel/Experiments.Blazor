# SPACats BlazorServer

This project is based on the [Cofoundry SPA example application](https://github.com/cofoundry-cms/Cofoundry.Samples.SPASite), ported from VueJs into Blazor. This variant of the app uses the interactive server-side rendering render mode and direct sever-side data access.

If you want to run the project, follow the instructions on the [original Cofoundry sample repository](https://github.com/cofoundry-cms/Cofoundry.Samples.SPASite) to configure Cofoundry and initialize the data.

> Note that these applications are experiments and are not necessarily production ready code. Shortcuts or hacks may have been made to further the learning or experimentation process. 

## Points of note:

- Cookies cannot be set in InteractiveServer mode so we have to do login/registration pages in static render mode, or else build them in RazorPages.
- Mixed static and interactive server render with pre-render mode causes a few issues whereby state initialization code at the app level does not get re-initialized when the "page" server rendered component reloads, hence why we use `MemberState.EnsureLoaded` on each page that needs it. I expect there is a better way but I can't find any example showing global initialization of state on first load.
- Adding custom errors to `EditContext` is a bit convoluted compared to typical ASP.NET `ModelState`, and the validation controls don't let you have a "catch all" validation summary to show errors not displayed in inline field validators, which is a feature you also get in MVC.
- Blazor in interactive server mode isn't compatible with the way we do session/dbcontext management in Cofoundry (via scoped DI registrations) so I've had to override the default implementation of `ContentRespository` to ensure each command/query execution is in it's own scope, however this isn't necessarily optimal either as some things that take advantage of "request" scoped caching aren't utilized correctly if multiple command/queries are executed in the SignalR equivalent of an http "request" - UserContext caching in particular springs to mind.
