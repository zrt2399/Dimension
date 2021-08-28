﻿using DimensionService.Models;
using DimensionService.Models.RequestModels;
using DimensionService.Models.ResultModels;
using DimensionService.Service.Chat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace DimensionService.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        /// <summary>
        /// 添加新聊天
        /// </summary>
        /// <param name="data">请求数据</param>
        /// <returns></returns>
        [Route("AddChat")]
        [HttpPost]
        public WebResultModel AddChat(AddChatModel data)
        {
            Request.Headers.TryGetValue("UserID", out StringValues userID);
            data.UserID = userID;

            WebResultModel webResult = new()
            {
                State = _chatService.AddChat(data, out string message),
                Data = null,
                Message = message
            };
            return webResult;
        }

        /// <summary>
        /// 获取聊天列表
        /// </summary>
        /// <returns></returns>
        [Route("GetChatColumnInfo")]
        [HttpGet]
        public WebResultModel GetChatColumnInfo()
        {
            Request.Headers.TryGetValue("UserID", out StringValues userID);

            WebResultModel webResult = new()
            {
                State = _chatService.GetChatColumnInfo(userID, out List<ChatColumnInfoModel> chatColumnInfos, out string message),
                Data = chatColumnInfos,
                Message = message
            };
            return webResult;
        }
    }
}
