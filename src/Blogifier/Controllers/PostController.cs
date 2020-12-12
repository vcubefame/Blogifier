﻿using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		private readonly IPostProvider _postProvider;

		public PostController(IPostProvider postProvider)
		{
			_postProvider = postProvider;
		}

		[HttpGet("list/{filter}")]
		public async Task<ActionResult<List<Post>>> GetPosts(PublishedStatus filter)
		{
			return await _postProvider.GetPosts(filter);
		}

		[HttpGet("list/search/{term}")]
		public async Task<ActionResult<List<Post>>> SearchPosts(string term)
		{
			return await _postProvider.SearchPosts(term);
		}

		[HttpGet("byslug/{slug}")]
		public async Task<ActionResult<Post>> GetPostBySlug(string slug)
		{
			return await _postProvider.GetPostBySlug(slug);
		}

		[HttpGet("getslug/{title}")]
		public async Task<ActionResult<string>> GetSlug(string title)
		{
			return await _postProvider.GetSlugFromTitle(title);
		}

		[HttpPost("add")]
		public async Task<ActionResult<bool>> AddPost(Post post)
		{
			return await _postProvider.Add(post);
		}

		[HttpPut("update")]
		public async Task<ActionResult<bool>> UpdatePost(Post post)
		{
			return await _postProvider.Update(post);
		}

		[HttpPut("publish/{id:int}")]
		public async Task<ActionResult<bool>> PublishPost(int id, [FromBody] bool publish)
		{
			return await _postProvider.Publish(id, publish);
		}

		[HttpPut("featured/{id:int}")]
		public async Task<ActionResult<bool>> FeaturedPost(int id, [FromBody] bool featured)
		{
			return await _postProvider.Featured(id, featured);
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<bool>> RemovePost(int id)
		{
			return await _postProvider.Remove(id);
		}
	}
}
