// Skipping function Client_ShardConnected(none), it contains poisonous unsupported syntaxes

func @_Public_Bot.CommandHandler.FormatJson$string$(none) -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandHandler.cs" :58 :8) {
^entry (%_json : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandHandler.cs" :58 :41)
cbde.store %_json, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandHandler.cs" :58 :41)
br ^0

^0: // JumpBlock
// Entity from another assembly: JsonConvert
%1 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandHandler.cs" :60 :63) // Not a variable of known type: json
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandHandler.cs" :60 :33) // JsonConvert.DeserializeObject(json) (InvocationExpression)
// Entity from another assembly: JsonConvert
%4 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandHandler.cs" :61 :47) // Not a variable of known type: parsedJson
// Entity from another assembly: Formatting
%5 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandHandler.cs" :61 :59) // Formatting.Indented (SimpleMemberAccessExpression)
%6 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandHandler.cs" :61 :19) // JsonConvert.SerializeObject(parsedJson, Formatting.Indented) (InvocationExpression)
return %6 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandHandler.cs" :61 :12)

^1: // ExitBlock
cbde.unreachable

}
// Skipping function HandleFailedGql(none, none, none, none, none), it contains poisonous unsupported syntaxes

// Skipping function GetGuildSettings(none), it contains poisonous unsupported syntaxes

// Skipping function LoadGuildSettings(), it contains poisonous unsupported syntaxes

// Skipping function Ready(none), it contains poisonous unsupported syntaxes

// Skipping function IsBotRole(none), it contains poisonous unsupported syntaxes

// Skipping function CheckCommandAsync(none), it contains poisonous unsupported syntaxes

