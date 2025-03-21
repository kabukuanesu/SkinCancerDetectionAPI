using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkinCancerDetectionAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhysicalAddress = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateCreated = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateUpdated = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Authentications",
                columns: table => new
                {
                    AuthenticationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RecoveryQuestion = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RecoveryAnswer = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authentications", x => x.AuthenticationId);
                    table.ForeignKey(
                        name: "FK_Authentications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhysicalAddress = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MedicalHistory = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FamilyHistory = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SkinType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsScheduled = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Scheduled = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateCreated = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateUpdated = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    BookedDate = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lesions",
                columns: table => new
                {
                    LesionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    LesionLocation = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateOfDetection = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateOfExamination = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LesionDescription = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DermoscopicFeatures = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsBiopsy = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesions", x => x.LesionId);
                    table.ForeignKey(
                        name: "FK_Lesions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "Workflows",
                columns: table => new
                {
                    WorkflowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateCreated = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateUpdated = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflows", x => x.WorkflowId);
                    table.ForeignKey(
                        name: "FK_Workflows_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "Biopsies",
                columns: table => new
                {
                    BiopsyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LesionId = table.Column<int>(type: "int", nullable: false),
                    BiopsyDate = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    BiopsyResult = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    BiopsyResultDescription = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biopsies", x => x.BiopsyId);
                    table.ForeignKey(
                        name: "FK_Biopsies_Lesions_LesionId",
                        column: x => x.LesionId,
                        principalTable: "Lesions",
                        principalColumn: "LesionId");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LesionId = table.Column<int>(type: "int", nullable: false),
                    ImageType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ImageDescription = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Resolution = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateTaken = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CameraModel = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IlluminationType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateUpdated = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Lesions_LesionId",
                        column: x => x.LesionId,
                        principalTable: "Lesions",
                        principalColumn: "LesionId");
                });

            migrationBuilder.CreateTable(
                name: "WorkflowStages",
                columns: table => new
                {
                    WorkflowStageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowId = table.Column<int>(type: "int", nullable: false),
                    WorkflowStageName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AssignedTo = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    EndDate = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowStages", x => x.WorkflowStageId);
                    table.ForeignKey(
                        name: "FK_WorkflowStages_Workflows_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflows",
                        principalColumn: "WorkflowId");
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    DiagnosisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BiopsyId = table.Column<int>(type: "int", nullable: false),
                    DiagnosisDate = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DiagnosisResult = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DiagnosisDescription = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.DiagnosisId);
                    table.ForeignKey(
                        name: "FK_Diagnoses_Biopsies_BiopsyId",
                        column: x => x.BiopsyId,
                        principalTable: "Biopsies",
                        principalColumn: "BiopsyId");
                });

            migrationBuilder.CreateTable(
                name: "ModelPredictions",
                columns: table => new
                {
                    ModelPredictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    PredictionResult = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ConfidenceScore = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelPredictions", x => x.ModelPredictionId);
                    table.ForeignKey(
                        name: "FK_ModelPredictions_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId");
                });

            migrationBuilder.CreateTable(
                name: "FinalDiagnoses",
                columns: table => new
                {
                    FinalDiagnosisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiagnosisId = table.Column<int>(type: "int", nullable: false),
                    ModelPredictionId = table.Column<int>(type: "int", nullable: false),
                    FinalResult = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FinalResultDescription = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalDiagnoses", x => x.FinalDiagnosisId);
                    table.ForeignKey(
                        name: "FK_FinalDiagnoses_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "DiagnosisId");
                    table.ForeignKey(
                        name: "FK_FinalDiagnoses_ModelPredictions_ModelPredictionId",
                        column: x => x.ModelPredictionId,
                        principalTable: "ModelPredictions",
                        principalColumn: "ModelPredictionId");
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    TreatmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinalDiagnosisId = table.Column<int>(type: "int", nullable: false),
                    TreatmentDate = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TreatmentType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TreatmentDescription = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsFollowUp = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FollowUpDate = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.TreatmentId);
                    table.ForeignKey(
                        name: "FK_Treatments_FinalDiagnoses_FinalDiagnosisId",
                        column: x => x.FinalDiagnosisId,
                        principalTable: "FinalDiagnoses",
                        principalColumn: "FinalDiagnosisId");
                });

            migrationBuilder.CreateTable(
                name: "FollowUps",
                columns: table => new
                {
                    FollowUpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TreatmentId = table.Column<int>(type: "int", nullable: false),
                    FollowUpDate = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FollowUpResult = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FollowUpDescription = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsFollowUp = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FollowingUpDate = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUps", x => x.FollowUpId);
                    table.ForeignKey(
                        name: "FK_FollowUps_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "TreatmentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authentications_UserId",
                table: "Authentications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Biopsies_LesionId",
                table: "Biopsies",
                column: "LesionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PatientId",
                table: "Bookings",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_BiopsyId",
                table: "Diagnoses",
                column: "BiopsyId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalDiagnoses_DiagnosisId",
                table: "FinalDiagnoses",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalDiagnoses_ModelPredictionId",
                table: "FinalDiagnoses",
                column: "ModelPredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_TreatmentId",
                table: "FollowUps",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_LesionId",
                table: "Images",
                column: "LesionId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesions_PatientId",
                table: "Lesions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelPredictions_ImageId",
                table: "ModelPredictions",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_FinalDiagnosisId",
                table: "Treatments",
                column: "FinalDiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_PatientId",
                table: "Workflows",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStages_WorkflowId",
                table: "WorkflowStages",
                column: "WorkflowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authentications");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "FollowUps");

            migrationBuilder.DropTable(
                name: "WorkflowStages");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Workflows");

            migrationBuilder.DropTable(
                name: "FinalDiagnoses");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "ModelPredictions");

            migrationBuilder.DropTable(
                name: "Biopsies");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Lesions");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
