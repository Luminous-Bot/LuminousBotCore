// Skipping function Init(none), it contains poisonous unsupported syntaxes

// Skipping function GetOrCreateGuildMember(none, none), it contains poisonous unsupported syntaxes

// Skipping function GuildMemberExists(none, none), it contains poisonous unsupported syntaxes

// Skipping function CreateGuildMember(none, none), it contains poisonous unsupported syntaxes

// Skipping function GetGuildMember(none, none), it contains poisonous unsupported syntaxes

// Skipping function NewUser(none), it contains poisonous unsupported syntaxes

// Skipping function CheckGuilds(), it contains poisonous unsupported syntaxes

// Skipping function AddGuild(none), it contains poisonous unsupported syntaxes

func @_Public_Bot.GuildHandler.Addguild$Discord.WebSocket.SocketGuild$(none) -> () loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\GuildHandler.cs" :122 :8) {
^entry (%_arg : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\GuildHandler.cs" :122 :36)
cbde.store %_arg, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\GuildHandler.cs" :122 :36)
br ^0

^0: // SimpleBlock
%1 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\GuildHandler.cs" :124 :30) // Not a variable of known type: arg
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\GuildHandler.cs" :124 :20) // new Guild(arg) (ObjectCreationExpression)
%4 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\GuildHandler.cs" :125 :12) // Not a variable of known type: CurrentGuilds
%5 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\GuildHandler.cs" :125 :30) // Not a variable of known type: g
%6 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\GuildHandler.cs" :125 :12) // CurrentGuilds.Add(g) (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: LevelHandler
%7 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\GuildHandler.cs" :126 :12) // LevelHandler.GuildLevels (SimpleMemberAccessExpression)
%8 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\GuildHandler.cs" :126 :41) // Not a variable of known type: g
%9 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\GuildHandler.cs" :126 :41) // g.Leaderboard (SimpleMemberAccessExpression)
%10 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\GuildHandler.cs" :126 :12) // LevelHandler.GuildLevels.Add(g.Leaderboard) (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
// Skipping function GetGuild(none), it contains poisonous unsupported syntaxes

