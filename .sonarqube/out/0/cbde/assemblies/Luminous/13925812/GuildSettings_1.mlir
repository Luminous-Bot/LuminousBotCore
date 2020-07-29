func @_Public_Bot.GuildSettings.SaveGuildSettings$$() -> () loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :27 :8) {
^entry :
br ^0

^0: // SimpleBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: StateHandler
%0 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :29 :57) // "guildsettings" (StringLiteralExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: CommandHandler
%1 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :29 :74) // CommandHandler.CurrentGuildSettings (SimpleMemberAccessExpression)
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :29 :12) // StateHandler.SaveObject<List<GuildSettings>>("guildsettings", CommandHandler.CurrentGuildSettings) (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
func @_Public_Bot.GuildSettings.AddRole$Discord.WebSocket.SocketRole$(none) -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :31 :8) {
^entry (%_role : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :31 :37)
cbde.store %_role, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :31 :37)
br ^0

^0: // BinaryBranchBlock
%1 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :33 :17) // this (ThisExpression)
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :33 :17) // this.PermissionRoles (SimpleMemberAccessExpression)
%3 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :33 :47) // Not a variable of known type: role
%4 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :33 :47) // role.Id (SimpleMemberAccessExpression)
%5 = cbde.unknown : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :33 :17) // this.PermissionRoles.Contains(role.Id) (InvocationExpression)
%6 = cbde.unknown : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :33 :16) // !this.PermissionRoles.Contains(role.Id) (LogicalNotExpression)
cond_br %6, ^1, ^2 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :33 :16)

^1: // SimpleBlock
%7 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :34 :16) // this (ThisExpression)
%8 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :34 :16) // this.PermissionRoles (SimpleMemberAccessExpression)
%9 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :34 :41) // Not a variable of known type: role
%10 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :34 :41) // role.Id (SimpleMemberAccessExpression)
%11 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :34 :16) // this.PermissionRoles.Add(role.Id) (InvocationExpression)
br ^3

^2: // JumpBlock
%12 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :35 :23) // new Exception() (ObjectCreationExpression)
cbde.throw %12 :  none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :35 :17)

^3: // JumpBlock
%13 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :36 :19) // this (ThisExpression)
return %13 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :36 :12)

^4: // ExitBlock
cbde.unreachable

}
func @_Public_Bot.GuildSettings.RemoveRole$Discord.WebSocket.SocketRole$(none) -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :38 :8) {
^entry (%_role : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :38 :40)
cbde.store %_role, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :38 :40)
br ^0

^0: // BinaryBranchBlock
%1 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :40 :16) // this (ThisExpression)
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :40 :16) // this.PermissionRoles (SimpleMemberAccessExpression)
%3 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :40 :46) // Not a variable of known type: role
%4 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :40 :46) // role.Id (SimpleMemberAccessExpression)
%5 = cbde.unknown : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :40 :16) // this.PermissionRoles.Contains(role.Id) (InvocationExpression)
cond_br %5, ^1, ^2 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :40 :16)

^1: // SimpleBlock
%6 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :41 :16) // this (ThisExpression)
%7 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :41 :16) // this.PermissionRoles (SimpleMemberAccessExpression)
%8 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :41 :44) // Not a variable of known type: role
%9 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :41 :44) // role.Id (SimpleMemberAccessExpression)
%10 = cbde.unknown : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :41 :16) // this.PermissionRoles.Remove(role.Id) (InvocationExpression)
br ^3

^2: // JumpBlock
%11 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :42 :23) // new Exception() (ObjectCreationExpression)
cbde.throw %11 :  none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :42 :17)

^3: // JumpBlock
%12 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :43 :19) // this (ThisExpression)
return %12 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\GuildSettings.cs" :43 :12)

^4: // ExitBlock
cbde.unreachable

}
