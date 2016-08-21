using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EmptyQueryNativeTest;

namespace EmptyQueryNativeTest.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20160821102227_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("EmptyQueryNativeTest.Child", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ParentId");

                    b.Property<string>("SomeString");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("EmptyQueryNativeTest.Model", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("SomeString");

                    b.HasKey("Id");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("EmptyQueryNativeTest.Child", b =>
                {
                    b.HasOne("EmptyQueryNativeTest.Model", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
