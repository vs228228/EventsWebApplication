﻿using EventsWebApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsWebApplication.Infrastructure.Configurations
{
    public class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
    {
        public void Configure(EntityTypeBuilder<EventParticipant> builder)
        {
            builder.HasKey(ep => new { ep.UserId, ep.EventId });

            builder.Property(ep => ep.UserId)
                .ValueGeneratedNever();

            builder.Property(ep => ep.EventId)
                .ValueGeneratedNever();

            builder.HasOne(ep => ep.User)
                .WithMany(u => u.Events)
                .HasForeignKey(ep => ep.UserId);

            builder.HasOne(ep => ep.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(ep => ep.EventId);
        }
    }
}
