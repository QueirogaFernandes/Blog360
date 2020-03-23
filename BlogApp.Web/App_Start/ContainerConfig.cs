using Autofac;
using Autofac.Integration.Mvc;
using BlogApp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BlogApp.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<UserService>()
                   .As<IUserService>()
                   .InstancePerRequest();
            builder.RegisterType<PostService>()
                   .As<IPostsService>()
                   .InstancePerRequest();
            builder.RegisterType<CommentService>()
                   .As<ICommentService>()
                   .InstancePerRequest();
            builder.RegisterType<BlogAppDbContext>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}