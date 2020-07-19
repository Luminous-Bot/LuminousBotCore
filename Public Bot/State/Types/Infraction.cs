using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    public class Infraction
    {
        [GraphQLSVar, GraphQLName("Id")]
        public string InfracId { get; set; }
        [GraphQLProp, GraphQLSVar]
        public ulong GuildID { get; set; }
        [GraphQLProp, GraphQLSVar]
        public ulong MemberID { get; set; }
        [GraphQLProp, GraphQLSVar]
        public ulong ModeratorID { get; set; }
        [GraphQLProp, GraphQLSVar]
        public Action Action { get; set; }
        [GraphQLProp, GraphQLSVar]
        public DateTime Time { get; set; }
        [GraphQLProp, GraphQLSVar]
        public string Reason { get; set; }

        public Infraction() { }
        public Infraction(ulong MemberId, ulong ModeratorId, ulong GuildID, Action action, string Reason, DateTime time)
        {
            this.InfracId = genId();
            this.MemberID = MemberId;
            this.ModeratorID = ModeratorId;
            this.GuildID = GuildID;
            this.Action = action;
            this.Reason = Reason;
            this.Time = time;
            var guild = GuildHandler.GetGuild(GuildID);
            if (guild != null)
            {
                GuildMember member;
                if (guild.GuildMembers.Any(x => x.UserID == this.MemberID))
                    member = guild.GuildMembers.Find(x => x.UserID == this.MemberID);
                else
                {
                    var gm = GuildHandler.client.GetGuild(GuildID).GetUser(this.MemberID);
                    if (gm != null)
                        member = new GuildMember(gm);
                    else
                        throw new Exception("User is not in guild or list!");
                }
                member.Infractions.Add(this);
                StateService.Mutate<Infraction>(GraphQLParser.GenerateGQLMutation<Infraction>("createInfraction", true, this, "data", "CreateInfractionInput!"));
            }
            else
            {
                Logger.Write("Guild doesn't exist in GuildHandler!", Logger.Severity.Critical);
            }
        }
        private string genId()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[20];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
    }
}
