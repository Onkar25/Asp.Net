using System;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MessagesController(IMessageRepository messageRepository, IUserRepository userRepository, IMapper mapper) : BaseApiContoller
{
    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessage)
    {
        var userName = User.GetUsername();
        if (userName == createMessage.RecipientUsername.ToLower())
            return BadRequest("You cannot message yourself !!!");

        var sender = await userRepository.GetUserByUsernameAsync(userName);
        var recipient = await userRepository.GetUserByUsernameAsync(createMessage.RecipientUsername);

        if (sender == null || recipient == null) return BadRequest("Cannot send message at this time");

        var message = new Message
        {
            Sender = sender,
            Recipient = recipient,
            SenderUsername = sender.UserName,
            RecipientUsername = recipient.UserName,
            Conten = createMessage.Content
        };

        messageRepository.AddMessage(message);

        if (await messageRepository.SaveAllAsync()) return Ok(mapper.Map<MessageDto>(message));

        return BadRequest("Failed to save message");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageForUser([FromQuery]MessageParams messageParams)
    {
        messageParams.UserName = User.GetUsername();
        var message = await messageRepository.GetMessageForUser(messageParams);
        Response.AddPaginationHeader(message);

        return message;
    }

    [HttpGet("thread/{username}")]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
    {
        var currentUsername = User.GetUsername();
        return Ok(await messageRepository.GetMessageThread(currentUsername , username));
    }
}
