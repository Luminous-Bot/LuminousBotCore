func @_Public_Bot.Modules.Handlers.MuteHandler.SaveMuted$$() -> () loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\MuteHandler.cs" :41 :8) {
^entry :
br ^0

^0: // SimpleBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: StateHandler
%0 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\MuteHandler.cs" :43 :53) // "Muted" (StringLiteralExpression)
%1 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\MuteHandler.cs" :43 :62) // Not a variable of known type: CurrentMuted
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\MuteHandler.cs" :43 :12) // StateHandler.SaveObject<List<MutedUser>>("Muted", CurrentMuted) (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
// Skipping function LoadMuted(), it contains poisonous unsupported syntaxes

// Skipping function SetupMutedRole(none, none, none), it contains poisonous unsupported syntaxes

// Skipping function Unmute(none), it contains poisonous unsupported syntaxes

// Skipping function AddNewMuted(none, none, none), it contains poisonous unsupported syntaxes

