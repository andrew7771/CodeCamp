using AutoMapper;
using CoreCodeCamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCodeCamp.Data
{
    public class CampsProfile: Profile
    {
        public CampsProfile()
        {
            CreateMap<Camp, CampModel>()
                .ForMember(c => c.Venue, o => o.MapFrom(m => m.Location.VenueName));
            CreateMap<Talk, TalkModel>();
            CreateMap<Speaker, SpeakerModel>();
        }
    }
}
