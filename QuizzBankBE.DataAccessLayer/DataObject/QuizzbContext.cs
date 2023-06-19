using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuizzBankBE.DataAccessLayer.DataObject;

public partial class QuizzbContext : DbContext
{
    public QuizzbContext()
    {
    }

    public QuizzbContext(DbContextOptions<QuizzbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Keyword> Keywords { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionBankEntry> QuestionBankEntries { get; set; }

    public virtual DbSet<QuestionCategory> QuestionCategories { get; set; }

    public virtual DbSet<QuestionVersion> QuestionVersions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<QuizResponse> QuizResponses { get; set; }

    public virtual DbSet<QuizUserAccess> QuizUserAccesses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCategory> UserCategories { get; set; }

    public virtual DbSet<UserCourse> UserCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;user=root;password=12345678;database=quizzb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Idanswers).HasName("PRIMARY");

            entity.ToTable("answers");

            entity.HasIndex(e => e.Questionid, "fk_question_idx");

            entity.Property(e => e.Idanswers).HasColumnName("idanswers");
            entity.Property(e => e.Content)
                .HasColumnType("mediumtext")
                .HasColumnName("content");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("createdat");
            entity.Property(e => e.Fraction).HasColumnName("fraction");
            entity.Property(e => e.Questionid).HasColumnName("questionid");
            entity.Property(e => e.Updatedat)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.Questionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_question");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Courseid).HasName("PRIMARY");

            entity.ToTable("courses");

            entity.Property(e => e.Courseid).HasColumnName("courseid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("createdat");
            entity.Property(e => e.Enddate)
                .HasColumnType("datetime")
                .HasColumnName("enddate");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.Shortname)
                .HasMaxLength(255)
                .HasColumnName("shortname");
            entity.Property(e => e.Startdate)
                .HasColumnType("datetime")
                .HasColumnName("startdate");
            entity.Property(e => e.Updatedat)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updatedat");
        });

        modelBuilder.Entity<Keyword>(entity =>
        {
            entity.HasKey(e => e.Idkeywords).HasName("PRIMARY");

            entity.ToTable("keywords");

            entity.Property(e => e.Idkeywords).HasColumnName("idkeywords");
            entity.Property(e => e.Content)
                .HasMaxLength(45)
                .HasColumnName("content");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("createdat");
            entity.Property(e => e.Updatedat)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updatedat");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Idquestions).HasName("PRIMARY");

            entity.ToTable("questions");

            entity.HasIndex(e => e.Createdby, "fk_user_1_idx");

            entity.HasIndex(e => e.Updatedby, "fk_user_update_idx");

            entity.Property(e => e.Idquestions).HasColumnName("idquestions");
            entity.Property(e => e.Content)
                .HasColumnType("mediumtext")
                .HasColumnName("content");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.DefaultMark).HasColumnName("default_mark");
            entity.Property(e => e.Generalfeedback)
                .HasColumnType("mediumtext")
                .HasColumnName("generalfeedback");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Questionstype)
                .HasMaxLength(45)
                .HasColumnName("questionstype");
            entity.Property(e => e.Updatedat)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.QuestionCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_create");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.QuestionUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_update");

            entity.HasMany(d => d.Keywords).WithMany(p => p.Questions)
                .UsingEntity<Dictionary<string, object>>(
                    "Questionkeyword",
                    r => r.HasOne<Keyword>().WithMany()
                        .HasForeignKey("Keywordid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_keywords"),
                    l => l.HasOne<Question>().WithMany()
                        .HasForeignKey("Questionid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_questions"),
                    j =>
                    {
                        j.HasKey("Questionid", "Keywordid").HasName("PRIMARY");
                        j.ToTable("questionkeywords");
                        j.HasIndex(new[] { "Keywordid" }, "fk_keywords_idx");
                        j.IndexerProperty<int>("Questionid").HasColumnName("questionid");
                        j.IndexerProperty<int>("Keywordid").HasColumnName("keywordid");
                    });
        });

        modelBuilder.Entity<QuestionBankEntry>(entity =>
        {
            entity.HasKey(e => e.IdquestionBankEntry).HasName("PRIMARY");

            entity.ToTable("question_bank_entry");

            entity.HasIndex(e => e.QuestionCategoryId, "fk_question_category_idx");

            entity.Property(e => e.IdquestionBankEntry).HasColumnName("idquestion_bank_entry");
            entity.Property(e => e.QuestionCategoryId).HasColumnName("question_category_id");

            entity.HasOne(d => d.QuestionCategory).WithMany(p => p.QuestionBankEntries)
                .HasForeignKey(d => d.QuestionCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_question_category");

            entity.HasMany(d => d.Quizzs).WithMany(p => p.Questions)
                .UsingEntity<Dictionary<string, object>>(
                    "QuestionQuiz",
                    r => r.HasOne<Quiz>().WithMany()
                        .HasForeignKey("QuizzId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_quiz_question"),
                    l => l.HasOne<QuestionBankEntry>().WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_question_quiz"),
                    j =>
                    {
                        j.HasKey("QuestionId", "QuizzId").HasName("PRIMARY");
                        j.ToTable("question_quiz");
                        j.HasIndex(new[] { "QuestionId" }, "fk_question_quiz_idx");
                        j.HasIndex(new[] { "QuizzId" }, "fk_quiz_idx");
                        j.IndexerProperty<int>("QuestionId").HasColumnName("question_id");
                        j.IndexerProperty<int>("QuizzId").HasColumnName("quizz_id");
                    });
        });

        modelBuilder.Entity<QuestionCategory>(entity =>
        {
            entity.HasKey(e => e.IdquestionCategories).HasName("PRIMARY");

            entity.ToTable("question_categories");

            entity.HasIndex(e => e.Parent, "fk_parent _idx");

            entity.Property(e => e.IdquestionCategories).HasColumnName("idquestion_categories");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Parent).HasColumnName("parent");

            entity.HasOne(d => d.ParentNavigation).WithMany(p => p.InverseParentNavigation)
                .HasForeignKey(d => d.Parent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_parent ");
        });

        modelBuilder.Entity<QuestionVersion>(entity =>
        {
            entity.HasKey(e => e.IdquestionVersions).HasName("PRIMARY");

            entity.ToTable("question_versions");

            entity.HasIndex(e => e.QuestionBankEntryId, "fk_bank_entry_idx");

            entity.HasIndex(e => e.QuestionId, "fk_question_idx");

            entity.Property(e => e.IdquestionVersions).HasColumnName("idquestion_versions");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.QuestionBankEntryId).HasColumnName("question_bank_entry_id");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.QuestionBankEntry).WithMany(p => p.QuestionVersions)
                .HasForeignKey(d => d.QuestionBankEntryId)
                .HasConstraintName("fk_bank_entry_version");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionVersions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_question_version");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Idquiz).HasName("PRIMARY");

            entity.ToTable("quiz");

            entity.HasIndex(e => e.Courseid, "fk_course_idx");

            entity.Property(e => e.Idquiz).HasColumnName("idquiz");
            entity.Property(e => e.Courseid).HasColumnName("courseid");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Intro)
                .HasColumnType("mediumtext")
                .HasColumnName("intro");
            entity.Property(e => e.IsPublic)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_public");
            entity.Property(e => e.MaxPoint).HasColumnName("max_point");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.NaveMethod)
                .HasMaxLength(45)
                .HasColumnName("nave_method");
            entity.Property(e => e.Overduehanding)
                .HasMaxLength(45)
                .HasColumnName("overduehanding");
            entity.Property(e => e.PointToPass).HasColumnName("point_to_pass");
            entity.Property(e => e.PreferedBehavior)
                .HasMaxLength(45)
                .HasColumnName("prefered_behavior");
            entity.Property(e => e.TimeClose)
                .HasColumnType("datetime")
                .HasColumnName("time_close");
            entity.Property(e => e.TimeLimit)
                .HasMaxLength(45)
                .HasColumnName("time_limit");
            entity.Property(e => e.TimeOpen)
                .HasColumnType("datetime")
                .HasColumnName("time_open");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Course).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.Courseid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_course_quiz");
        });

        modelBuilder.Entity<QuizResponse>(entity =>
        {
            entity.HasKey(e => e.IdquizResponses).HasName("PRIMARY");

            entity.ToTable("quiz_responses");

            entity.HasIndex(e => e.QuestionId, "fk_quizrespones_questions_idx");

            entity.HasIndex(e => e.QuizId, "fk_quizrespones_quiz_idx");

            entity.HasIndex(e => e.ResponsesId, "fk_quizrespones_responseanswers_idx");

            entity.HasIndex(e => e.UserId, "fk_quizrespones_users_idx");

            entity.Property(e => e.IdquizResponses).HasColumnName("idquiz_responses");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.QuizId).HasColumnName("quiz_id");
            entity.Property(e => e.ResponsesId).HasColumnName("responses_id");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Question).WithMany(p => p.QuizResponses)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_quizrespones_questions");

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizResponses)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_quizrespones_quiz");

            entity.HasOne(d => d.Responses).WithMany(p => p.QuizResponses)
                .HasForeignKey(d => d.ResponsesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_quizrespones_responseanswers");

            entity.HasOne(d => d.User).WithMany(p => p.QuizResponses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_quizrespones_users");
        });

        modelBuilder.Entity<QuizUserAccess>(entity =>
        {
            entity.HasKey(e => e.IdquizUserAccess).HasName("PRIMARY");

            entity.ToTable("quiz_user_access");

            entity.HasIndex(e => e.QuizId, "fk_quizuseraccess_quiz_idx");

            entity.HasIndex(e => e.UserId, "fk_quizuseraccess_user_idx");

            entity.HasIndex(e => e.AddBy, "fk_quizuseraccess_useradd_idx");

            entity.Property(e => e.IdquizUserAccess).HasColumnName("idquiz_user_access");
            entity.Property(e => e.AddAt)
                .HasColumnType("datetime")
                .HasColumnName("add_at");
            entity.Property(e => e.AddBy).HasColumnName("add_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.QuizId).HasColumnName("quiz_id");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.AddByNavigation).WithMany(p => p.QuizUserAccessAddByNavigations)
                .HasForeignKey(d => d.AddBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_quizuseraccess_useradd");

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizUserAccesses)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_quizuseraccess_quiz");

            entity.HasOne(d => d.User).WithMany(p => p.QuizUserAccessUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_quizuseraccess_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("createdat");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .HasColumnName("firstname");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");
            entity.Property(e => e.Updatedat)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updatedat");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserCategory>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CategoryId }).HasName("PRIMARY");

            entity.ToTable("user_categories");

            entity.HasIndex(e => e.AddBy, "fk_categories_user_add");

            entity.HasIndex(e => e.CategoryId, "fk_categories_users_idx");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.AddBy).HasColumnName("add_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Permission).HasColumnName("permission");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.AddByNavigation).WithMany(p => p.UserCategoryAddByNavigations)
                .HasForeignKey(d => d.AddBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_categories_user_add");

            entity.HasOne(d => d.Category).WithMany(p => p.UserCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_categories_users");

            entity.HasOne(d => d.User).WithMany(p => p.UserCategoryUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_categories");
        });

        modelBuilder.Entity<UserCourse>(entity =>
        {
            entity.HasKey(e => e.UserCoursesId).HasName("PRIMARY");

            entity.ToTable("user_courses");

            entity.HasIndex(e => e.CoursesId, "fk_course_idx");

            entity.HasIndex(e => e.UserId, "fk_user");

            entity.Property(e => e.UserCoursesId).HasColumnName("user_courses_id");
            entity.Property(e => e.CoursesId).HasColumnName("courses_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Role)
                .HasMaxLength(45)
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Courses).WithMany(p => p.UserCourses)
                .HasForeignKey(d => d.CoursesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_course");

            entity.HasOne(d => d.User).WithMany(p => p.UserCourses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
