using BlogApp.Data;
using BlogApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp
{
    public class PostModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var post = (Post)base.BindModel(controllerContext, bindingContext);

            var _unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>();

            if (post.Category != null)
            {
                post.Category = _unitOfWork.Categories.GetCategoryByID(post.Category.ID);
                post.CategoryId = post.Category.ID;
            }

            var tags = bindingContext.ValueProvider.GetValue("Tags").AttemptedValue.Split(',');

            if(tags.Length > 0)
            {
                post.Tags = new List<Tag>();

                foreach (var tag in tags)
                {
                    post.Tags.Add(_unitOfWork.Tags.GetTagByID(int.Parse(tag.Trim())));
                }
            }

            if (bindingContext.ValueProvider.GetValue("oper").AttemptedValue.Equals("edit"))
                post.Modified = DateTime.UtcNow;
            else
                post.PostedOn = DateTime.UtcNow;

            _unitOfWork.Dispose();

            return post;
        }
    }
}