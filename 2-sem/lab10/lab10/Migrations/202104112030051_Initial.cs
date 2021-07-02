namespace lab10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        SpecId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Specs", t => t.SpecId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.SpecId);
            
            CreateTable(
                "dbo.Specs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        AvgGrade = c.Double(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.SpecCourses",
                c => new
                    {
                        Spec_Id = c.Int(nullable: false),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Spec_Id, t.Course_Id })
                .ForeignKey("dbo.Specs", t => t.Spec_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Spec_Id)
                .Index(t => t.Course_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Groups", "SpecId", "dbo.Specs");
            DropForeignKey("dbo.SpecCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.SpecCourses", "Spec_Id", "dbo.Specs");
            DropForeignKey("dbo.Groups", "CourseId", "dbo.Courses");
            DropIndex("dbo.SpecCourses", new[] { "Course_Id" });
            DropIndex("dbo.SpecCourses", new[] { "Spec_Id" });
            DropIndex("dbo.Students", new[] { "GroupId" });
            DropIndex("dbo.Groups", new[] { "SpecId" });
            DropIndex("dbo.Groups", new[] { "CourseId" });
            DropTable("dbo.SpecCourses");
            DropTable("dbo.Students");
            DropTable("dbo.Specs");
            DropTable("dbo.Groups");
            DropTable("dbo.Courses");
        }
    }
}
