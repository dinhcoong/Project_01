using System.Web;
using System.Web.Optimization;

namespace mvcweb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
            //one tech theme
            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/AppFiles/Theme/resource/bootstrap.min.css",
                     "~/AppFiles/Theme/resource/animate.css",
                     "~/AppFiles/Theme/resource/main_styles.css",
                     "~/AppFiles/Theme/resource/owl.carousel.css",
                     "~/AppFiles/Theme/resource/owl.theme.default.css",
                     "~/AppFiles/Theme/resource/responsive.css",
                     "~/AppFiles/Theme/resource/slick.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Content/js").Include(
                   "~/AppFiles/Theme/js/bootstrap.min.css",
                   "~/AppFiles/Theme/js/popper.js",
                   "~/AppFiles/Theme/js/bootstrap.min.js",
                   "~/AppFiles/Theme/js/TweenMax.min.js",
                   "~/AppFiles/Theme/js/ScrollMagic.min.js",
                   "~/AppFiles/Theme/js/animation.gsap.min.js",
                    "~/AppFiles/Theme/js/ScrollToPlugin.min.js",
                     "~/AppFiles/Theme/js/animation.gsap.min.js",
                     "~/AppFiles/Theme/js/owl.carousel.js",
                     "~/AppFiles/Theme/js/slick.js",
                        "~/AppFiles/Theme/js/custom.js",
                   "~/AppFiles/Theme/js/easing.js"));

            //< script src = "js/jquery-3.3.1.min.js" ></ script >
            // < script src = "styles/bootstrap4/popper.js" ></ script >
            //  < script src = "styles/bootstrap4/bootstrap.min.js" ></ script >
            //   < script src = "plugins/greensock/TweenMax.min.js" ></ script >
            //    < script src = "plugins/greensock/TimelineMax.min.js" ></ script >
            //     < script src = "plugins/scrollmagic/ScrollMagic.min.js" ></ script >
            //      < script src = "plugins/greensock/animation.gsap.min.js" ></ script >
            //       < script src = "plugins/greensock/ScrollToPlugin.min.js" ></ script >
            //        < script src = "plugins/OwlCarousel2-2.2.1/owl.carousel.js" ></ script >
            //         < script src = "plugins/slick-1.8.0/slick.js" ></ script >
            //          < script src = "plugins/easing/easing.js" ></ script >
            //           < script src = "js/custom.js" ></ script >
        }
    }
}
