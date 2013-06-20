using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Splintered_Universe.Models;
using Splintered_Universe.Models.VM;
using AutoMapper;

namespace Splintered_Universe.Models
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new SystemSkillsProfile());
            });
        }
    }

    public class SystemSkillsProfile : Profile
    {
        protected override void Configure()
        {

            Mapper.CreateMap<SystemRule, SystemRuleVM>();
            Mapper.CreateMap<SystemRuleVM, SystemRule>()
                .ForMember(dest => dest.Campaigns, opt => opt.Ignore())
                .ForMember(dest => dest.SystemActionCategories, opt => opt.Ignore())
                .ForMember(dest => dest.SystemSkillCategories, opt => opt.Ignore())
                .ForMember(dest => dest.SystemStats, opt => opt.Ignore());

            Mapper.CreateMap<SystemSkillCategory, SystemSkillCategoryVM>();
            Mapper.CreateMap<SystemSkillCategoryVM, SystemSkillCategory>()
                .ForMember(dest => dest.SystemSkills, opt => opt.Ignore())
                .ForMember(dest => dest.SystemRule, opt => opt.Ignore());

            Mapper.CreateMap<SystemSkill, SystemSkillVM>();
            Mapper.CreateMap<SystemSkillVM, SystemSkill>()
                .ForMember(dest => dest.SystemStat, opt => opt.Ignore())
                .ForMember(dest => dest.SystemSkillCategory, opt => opt.Ignore())
                .ForMember(dest => dest.SystemAction, opt => opt.Ignore());

            Mapper.CreateMap<SystemActionCategory, SystemActionCategoryVM>();
            Mapper.CreateMap<SystemActionCategoryVM, SystemActionCategory>()
                .ForMember(dest => dest.SystemActions, opt => opt.Ignore())
                .ForMember(dest => dest.SystemRule, opt => opt.Ignore());

            Mapper.CreateMap<SystemAction, SystemActionVM>();
            Mapper.CreateMap<SystemActionVM, SystemAction>()
                .ForMember(dest => dest.CombatEncounterItems, opt => opt.Ignore())
                .ForMember(dest => dest.SystemSkills, opt => opt.Ignore())
                .ForMember(dest => dest.SystemActionCategory, opt => opt.Ignore());

            Mapper.CreateMap<SystemStat, SystemStatVM>();
            Mapper.CreateMap<SystemStatVM, SystemStat>()
                .ForMember(dest => dest.SystemStatLevels, opt => opt.Ignore())
                .ForMember(dest => dest.SystemSkills, opt => opt.Ignore())
                .ForMember(dest => dest.SystemRule, opt => opt.Ignore());

            Mapper.CreateMap<SystemStatLevel, SystemStatLevelVM>();
            Mapper.CreateMap<SystemStatLevelVM, SystemStatLevel>()
                .ForMember(dest => dest.SystemStat, opt => opt.Ignore());

        }
    }
}