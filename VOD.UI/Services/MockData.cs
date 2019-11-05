using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOD.Common.Entities;

namespace VOD.UI.Services
{
    public class MockData
    {
        public List<Instructor> Instructors;
        public List<Course> Courses;
        public List<UserCourse> UserCourses;
        public List<Module> Modules;
        public List<Video> Videos;
        public List<Download> Downloads;

        public MockData()
        {
            SeedData();
        }

        private void SeedData()
        {
            #region Lorem Ipsum - Dummy Data
            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
            #endregion

            Instructors = new List<Instructor> {
                new Instructor {
                    Id = 1,
                    Name = "John Doe",
                    Description = description.Substring(20, 50),
                    Thumbnail = "/images/Ice-Age-Scrat-icon.png"
                },
                new Instructor {
                    Id = 2,
                    Name = "Jane Doe",
                    Description = description.Substring(30, 40),
                    Thumbnail = "/images/Ice-Age-Scrat-icon.png"
                } };
            Courses = new List<Course>{
                new Course {
                    Id = 1,
                    InstructorId = 1,
                    Title = "Course 1",
                    Description = description,
                    ImageUrl = "/images/course1.jpg",
                    MarqueeImageUrl = "/images/laptop.jpg"
                },
                new Course {
                    Id = 2,
                    InstructorId = 2,
                    Title = "Course 2",
                    Description = description,
                    ImageUrl = "/images/course2.jpg",
                    MarqueeImageUrl = "/images/laptop.jpg"
                },
                new Course {
                    Id = 3,
                    InstructorId = 1,
                    Title = "Course 3",
                    Description = description,
                    ImageUrl = "/images/course3.jpg",
                    MarqueeImageUrl = "/images/laptop.jpg"
                }};
            UserCourses = new List<UserCourse> {
                new UserCourse { UserId = "123", CourseId = 1 },
                new UserCourse { UserId = "123", CourseId = 2 },
                new UserCourse { UserId = "123", CourseId = 3 }
            };
            Modules = new List<Module>{
                new Module { Id = 1, Course = Courses.Single(c => c.Id.Equals(1)), CourseId = 1, Title = "Modeule 1" },
                new Module { Id = 2,  Course = Courses.Single(c => c.Id.Equals(1)), CourseId = 1, Title = "Modeule 2" },
                new Module { Id = 3,  Course = Courses.Single(c => c.Id.Equals(2)), CourseId = 2, Title = "Modeule 3" },
            };
            Videos = new List<Video> {
                new Video { 
                    Id = 1, ModuleId = 1, CourseId = 1,
                    Title = "Video 1 Title",
                    Description = description.Substring(1, 35),
                    Duration = 50, Thumbnail = "/images/video1.jpg",
                    Url = "https://www.youtube.com/embed/BJFyzpBcaCY"
                },
                new Video {
                    Id = 2, ModuleId = 1, CourseId = 1,
                    Title = "Video 2 Title",
                    Description = description.Substring(5, 35),
                    Duration = 45, Thumbnail = "/images/video2.jpg",
                    Url = "https://www.youtube.com/embed/BJFyzpBcaCY"
                },
                new Video {
                    Id = 3, ModuleId = 1, CourseId = 1,
                    Title = "Video 3 Title",
                    Description = description.Substring(10, 35),
                    Duration = 41, Thumbnail = "/images/video3.jpg",
                    Url = "https://www.youtube.com/embed/BJFyzpBcaCY"
                },
                new Video {
                    Id = 4, ModuleId = 3, CourseId = 2,
                    Title = "Video 4 Title",
                    Description = description.Substring(15, 35),
                    Duration = 41, Thumbnail = "/images/video4.jpg",
                    Url = "https://www.youtube.com/embed/BJFyzpBcaCY"
                },
                new Video {
                    Id = 5, ModuleId = 2, CourseId = 1,
                    Title = "Video 5 Title",
                    Description = description.Substring(20, 35),
                    Duration = 42, Thumbnail = "/images/video5.jpg",
                    Url = "https://www.youtube.com/embed/BJFyzpBcaCY"
                } };
            Downloads = new List<Download> {
                new Download{
                    Id = 1, ModuleId = 1, CourseId = 1,
                    Title = "ADO.NET 1 (PDF)", Url = "https://some-url" },

                new Download{
                    Id = 2, ModuleId = 1, CourseId = 1,
                    Title = "ADO.NET 2 (PDF)", Url = "https://some-url" },

                new Download{
                    Id = 3, ModuleId = 3, CourseId = 2,
                    Title = "ADO.NET 1 (PDF)", Url = "https://some-url" }
            };
        }
    }
}
