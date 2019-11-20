using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VOD.Common.DTOModels;
using VOD.Common.Entities;
using VOD.UI.Services;

namespace VOD.UI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly string _userId;
        private readonly IMapper _mapper;
        private readonly IUIReadService _db;

        public CommentsController(
        IHttpContextAccessor httpContextAccessor,
        UserManager<VODUser> userManager, IMapper mapper, IUIReadService db)
        {
            var user = httpContextAccessor.HttpContext.User;
            //_userId = userManager.GetUserId(user);
            _userId = "123";
            _mapper = mapper;
            _db = db;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IEnumerable<CommentDTO>> Create([FromBody]Comment comment)
        {
            var parentComment = await _db.GetCommentAsync((int)comment.ParentId);
            comment.CourseId = parentComment.CourseId;

            var comments = await _db.GetCommentsAsync(comment.CourseId);
            var commentDTOs = _mapper.Map<List<CommentDTO>>(comments.OrderBy(o => o.Id));
            return commentDTOs;
        }

        [HttpGet]
        public async Task<IEnumerable<CommentDTO>> Get()
        {
            var comments = await _db.GetCommentsAsync(1);
            var commentDTOs = _mapper.Map<List<CommentDTO>>(comments.OrderBy(o => o.Id));
            return commentDTOs;
        }

    }
}