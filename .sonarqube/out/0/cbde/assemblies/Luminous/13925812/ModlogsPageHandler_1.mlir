func @_Public_Bot.Modules.Handlers.ModlogsPageHandler.BuildHelpPage$System.Collections.Generic.List$Public_Bot.Infraction$.ulong.ulong.ulong.ulong$(none, none, none, none, none) -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :42 :8) {
^entry (%_logs : none, %_mID : none, %_uID : none, %_gID : none, %_pOwn : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :42 :51)
cbde.store %_logs, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :42 :51)
%1 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :42 :74)
cbde.store %_mID, %1 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :42 :74)
%2 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :42 :85)
cbde.store %_uID, %2 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :42 :85)
%3 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :42 :96)
cbde.store %_gID, %3 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :42 :96)
%4 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :42 :107)
cbde.store %_pOwn, %4 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :42 :107)
br ^0

^0: // JumpBlock
%5 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :44 :34) // new ModlogHelpPage()             {                 MessageID = mID,                 Modlogs = logs,                 page = 1,                 UserID = uID,                 GuildID = gID,                 pageOwner = pOwn             } (ObjectCreationExpression)
%6 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :46 :28) // Not a variable of known type: mID
%7 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :47 :26) // Not a variable of known type: logs
%8 = constant 1 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :48 :23)
%9 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :49 :25) // Not a variable of known type: uID
%10 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :50 :26) // Not a variable of known type: gID
%11 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :51 :28) // Not a variable of known type: pOwn
%13 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :53 :19) // Not a variable of known type: page
return %13 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :53 :12)

^1: // ExitBlock
cbde.unreachable

}
// Skipping function BuildHelpPageEmbed(none, i32), it contains poisonous unsupported syntaxes

func @_Public_Bot.Modules.Handlers.ModlogsPageHandler.SaveMLPages$$() -> () loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :95 :8) {
^entry :
br ^0

^0: // SimpleBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: StateHandler
%0 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :97 :58) // "MlPages" (StringLiteralExpression)
%1 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :97 :69) // Not a variable of known type: CurrentPages
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\ModlogsPageHandler.cs" :97 :12) // StateHandler.SaveObject<List<ModlogHelpPage>>("MlPages", CurrentPages) (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
// Skipping function HandleModlogsPage(none, none, none), it contains poisonous unsupported syntaxes

