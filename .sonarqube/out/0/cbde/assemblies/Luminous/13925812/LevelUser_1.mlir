func @_Public_Bot.LevelUser.DiscordColorFromHex$string$(none) -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :43 :8) {
^entry (%_hex : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :43 :49)
cbde.store %_hex, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :43 :49)
br ^0

^0: // JumpBlock
// Entity from another assembly: System
%1 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :45 :20) // System.Drawing.ColorTranslator (SimpleMemberAccessExpression)
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :45 :64) // Not a variable of known type: hex
%3 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :45 :60) // $"#{hex}" (InterpolatedStringExpression)
%4 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :45 :20) // System.Drawing.ColorTranslator.FromHtml($"#{hex}") (InvocationExpression)
%6 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :46 :40) // Not a variable of known type: c
%7 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :46 :40) // c.R (SimpleMemberAccessExpression)
%8 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :46 :45) // Not a variable of known type: c
%9 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :46 :45) // c.G (SimpleMemberAccessExpression)
%10 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :46 :50) // Not a variable of known type: c
%11 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :46 :50) // c.B (SimpleMemberAccessExpression)
%12 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :46 :22) // new Discord.Color(c.R, c.G, c.B) (ObjectCreationExpression)
%14 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :47 :19) // Not a variable of known type: fin
return %14 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\LevelUser.cs" :47 :12)

^1: // ExitBlock
cbde.unreachable

}
// Skipping function Save(), it contains poisonous unsupported syntaxes

