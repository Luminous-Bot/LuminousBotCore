func @_Public_Bot.WelcomeCard.BuildEmbed$Discord.WebSocket.SocketGuildUser.Discord.WebSocket.SocketGuild$(none, none) -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :19 :8) {
^entry (%_usr : none, %_g : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :19 :32)
cbde.store %_usr, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :19 :32)
%1 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :19 :53)
cbde.store %_g, %1 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :19 :53)
br ^0

^0: // JumpBlock
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :21 :29) // new EmbedBuilder()              {                  Author = new EmbedAuthorBuilder()                  {                      Name = usr.ToString(),                      IconUrl = usr.GetAvatarUrl()                  },                  Description = GenerateWelcomeMessage(usr, g),                  Color = CommandModuleBase.Blurple,                  Footer = new EmbedFooterBuilder() { Text = g.Name },              } (ObjectCreationExpression)
%3 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :23 :25) // new EmbedAuthorBuilder()                  {                      Name = usr.ToString(),                      IconUrl = usr.GetAvatarUrl()                  } (ObjectCreationExpression)
%4 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :25 :27) // Not a variable of known type: usr
%5 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :25 :27) // usr.ToString() (InvocationExpression)
%6 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :26 :30) // Not a variable of known type: usr
%7 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :26 :30) // usr.GetAvatarUrl() (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: GenerateWelcomeMessage
%8 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :28 :53) // Not a variable of known type: usr
%9 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :28 :58) // Not a variable of known type: g
%10 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :28 :30) // GenerateWelcomeMessage(usr, g) (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: CommandModuleBase
%11 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :29 :24) // CommandModuleBase.Blurple (SimpleMemberAccessExpression)
%12 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :30 :25) // new EmbedFooterBuilder() { Text = g.Name } (ObjectCreationExpression)
%13 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :30 :59) // Not a variable of known type: g
%14 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :30 :59) // g.Name (SimpleMemberAccessExpression)
%15 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :21 :29) // new EmbedBuilder()              {                  Author = new EmbedAuthorBuilder()                  {                      Name = usr.ToString(),                      IconUrl = usr.GetAvatarUrl()                  },                  Description = GenerateWelcomeMessage(usr, g),                  Color = CommandModuleBase.Blurple,                  Footer = new EmbedFooterBuilder() { Text = g.Name },              }.WithCurrentTimestamp() (InvocationExpression)
%17 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :32 :19) // Not a variable of known type: b
%18 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :32 :19) // b.Build() (InvocationExpression)
return %18 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\WelcomeCard.cs" :32 :12)

^1: // ExitBlock
cbde.unreachable

}
