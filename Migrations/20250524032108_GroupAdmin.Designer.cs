﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialMediaApp.Models;

#nullable disable

namespace SocialMediaApp.Migrations
{
    [DbContext(typeof(SocialContext))]
    [Migration("20250524032108_GroupAdmin")]
    partial class GroupAdmin
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SocialMediaApp.Models.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SocialMediaApp.Models.Friend", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("FriendId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId1")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FriendId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("SocialMediaApp.Models.FriendRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("ReceiverId")
                        .HasColumnType("bigint");

                    b.Property<long>("SenderId")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("FriendRequests");
                });

            modelBuilder.Entity("SocialMediaApp.Models.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("CoverPicId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("GroupAdminId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PrivacyStateId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CoverPicId");

                    b.HasIndex("GroupAdminId");

                    b.HasIndex("PrivacyStateId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SocialMediaApp.Models.GroupMember", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("JoinedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupMembers");
                });

            modelBuilder.Entity("SocialMediaApp.Models.Image", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("PrivacyStateId")
                        .HasColumnType("bigint");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId1")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PrivacyStateId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("SocialMediaApp.Models.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ReceiverId")
                        .HasColumnType("bigint");

                    b.Property<long>("SenderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("SocialMediaApp.Models.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ImageId")
                        .HasColumnType("bigint");

                    b.Property<long>("PrivacyStateId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("ImageId");

                    b.HasIndex("PrivacyStateId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("SocialMediaApp.Models.PrivacyState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PrivacyStates");
                });

            modelBuilder.Entity("SocialMediaApp.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("CoverPicId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PrivacyStateId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProfilePicId")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CoverPicId");

                    b.HasIndex("PrivacyStateId");

                    b.HasIndex("ProfilePicId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SocialMediaApp.Models.Comment", b =>
                {
                    b.HasOne("SocialMediaApp.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaApp.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialMediaApp.Models.Friend", b =>
                {
                    b.HasOne("SocialMediaApp.Models.User", "FriendUser")
                        .WithMany()
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SocialMediaApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SocialMediaApp.Models.User", null)
                        .WithMany("Friends")
                        .HasForeignKey("UserId1");

                    b.Navigation("FriendUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialMediaApp.Models.FriendRequest", b =>
                {
                    b.HasOne("SocialMediaApp.Models.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaApp.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("SocialMediaApp.Models.Group", b =>
                {
                    b.HasOne("SocialMediaApp.Models.Image", "CoverPic")
                        .WithMany()
                        .HasForeignKey("CoverPicId");

                    b.HasOne("SocialMediaApp.Models.User", "GroupAdmin")
                        .WithMany()
                        .HasForeignKey("GroupAdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaApp.Models.PrivacyState", "PrivacyState")
                        .WithMany("Groups")
                        .HasForeignKey("PrivacyStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoverPic");

                    b.Navigation("GroupAdmin");

                    b.Navigation("PrivacyState");
                });

            modelBuilder.Entity("SocialMediaApp.Models.GroupMember", b =>
                {
                    b.HasOne("SocialMediaApp.Models.Group", "Group")
                        .WithMany("Members")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaApp.Models.User", "User")
                        .WithMany("GroupMemberships")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialMediaApp.Models.Image", b =>
                {
                    b.HasOne("SocialMediaApp.Models.PrivacyState", "PrivacyState")
                        .WithMany("Images")
                        .HasForeignKey("PrivacyStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaApp.Models.User", null)
                        .WithMany("Images")
                        .HasForeignKey("UserId1");

                    b.Navigation("PrivacyState");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialMediaApp.Models.Message", b =>
                {
                    b.HasOne("SocialMediaApp.Models.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaApp.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("SocialMediaApp.Models.Post", b =>
                {
                    b.HasOne("SocialMediaApp.Models.Group", "Group")
                        .WithMany("Posts")
                        .HasForeignKey("GroupId");

                    b.HasOne("SocialMediaApp.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("SocialMediaApp.Models.PrivacyState", "PrivacyState")
                        .WithMany("Posts")
                        .HasForeignKey("PrivacyStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaApp.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Image");

                    b.Navigation("PrivacyState");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialMediaApp.Models.User", b =>
                {
                    b.HasOne("SocialMediaApp.Models.Image", "CoverPic")
                        .WithMany()
                        .HasForeignKey("CoverPicId");

                    b.HasOne("SocialMediaApp.Models.PrivacyState", "PrivacyState")
                        .WithMany("Users")
                        .HasForeignKey("PrivacyStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaApp.Models.Image", "ProfilePic")
                        .WithMany()
                        .HasForeignKey("ProfilePicId");

                    b.Navigation("CoverPic");

                    b.Navigation("PrivacyState");

                    b.Navigation("ProfilePic");
                });

            modelBuilder.Entity("SocialMediaApp.Models.Group", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("SocialMediaApp.Models.Post", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("SocialMediaApp.Models.PrivacyState", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Images");

                    b.Navigation("Posts");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("SocialMediaApp.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Friends");

                    b.Navigation("GroupMemberships");

                    b.Navigation("Images");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
