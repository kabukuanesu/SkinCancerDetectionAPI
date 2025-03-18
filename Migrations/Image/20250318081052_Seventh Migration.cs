using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkinCancerDetectionAPI.Migrations.Image
{
    /// <inheritdoc />
    public partial class SeventhMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
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
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Authentication",
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
                    table.PrimaryKey("PK_Authentication", x => x.AuthenticationId);
                    table.ForeignKey(
                        name: "FK_Authentication_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
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
                    DateCreated = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateUpdated = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patient_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Lesion",
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
                    table.PrimaryKey("PK_Lesion", x => x.LesionId);
                    table.ForeignKey(
                        name: "FK_Lesion_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Biopsy",
                columns: table => new
                {
                    BiopsyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LesionId = table.Column<int>(type: "int", nullable: false),
                    BiopsyDate = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    BiopsyResult = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biopsy", x => x.BiopsyId);
                    table.ForeignKey(
                        name: "FK_Biopsy_Lesion_LesionId",
                        column: x => x.LesionId,
                        principalTable: "Lesion",
                        principalColumn: "LesionId",
                        onDelete: ReferentialAction.NoAction);
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
                        name: "FK_Images_Lesion_LesionId",
                        column: x => x.LesionId,
                        principalTable: "Lesion",
                        principalColumn: "LesionId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosis",
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
                    table.PrimaryKey("PK_Diagnosis", x => x.DiagnosisId);
                    table.ForeignKey(
                        name: "FK_Diagnosis_Biopsy_BiopsyId",
                        column: x => x.BiopsyId,
                        principalTable: "Biopsy",
                        principalColumn: "BiopsyId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ModelPrediction",
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
                    table.PrimaryKey("PK_ModelPrediction", x => x.ModelPredictionId);
                    table.ForeignKey(
                        name: "FK_ModelPrediction_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FinalDiagnosis",
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
                    table.PrimaryKey("PK_FinalDiagnosis", x => x.FinalDiagnosisId);
                    table.ForeignKey(
                        name: "FK_FinalDiagnosis_Diagnosis_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnosis",
                        principalColumn: "DiagnosisId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FinalDiagnosis_ModelPrediction_ModelPredictionId",
                        column: x => x.ModelPredictionId,
                        principalTable: "ModelPrediction",
                        principalColumn: "ModelPredictionId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Treatment",
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
                    table.PrimaryKey("PK_Treatment", x => x.TreatmentId);
                    table.ForeignKey(
                        name: "FK_Treatment_FinalDiagnosis_FinalDiagnosisId",
                        column: x => x.FinalDiagnosisId,
                        principalTable: "FinalDiagnosis",
                        principalColumn: "FinalDiagnosisId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FollowUp",
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
                    table.PrimaryKey("PK_FollowUp", x => x.FollowUpId);
                    table.ForeignKey(
                        name: "FK_FollowUp_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "TreatmentId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    AuditLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    LesionId = table.Column<int>(type: "int", nullable: false),
                    BiopsyId = table.Column<int>(type: "int", nullable: false),
                    DiagnosisId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    ModelPredictionId = table.Column<int>(type: "int", nullable: false),
                    FinalDiagnosisId = table.Column<int>(type: "int", nullable: false),
                    TreatmentId = table.Column<int>(type: "int", nullable: false),
                    FollowUpId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.AuditLogId);
                    table.ForeignKey(
                        name: "FK_AuditLog_Biopsy_BiopsyId",
                        column: x => x.BiopsyId,
                        principalTable: "Biopsy",
                        principalColumn: "BiopsyId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_Diagnosis_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnosis",
                        principalColumn: "DiagnosisId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_FinalDiagnosis_FinalDiagnosisId",
                        column: x => x.FinalDiagnosisId,
                        principalTable: "FinalDiagnosis",
                        principalColumn: "FinalDiagnosisId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_FollowUp_FollowUpId",
                        column: x => x.FollowUpId,
                        principalTable: "FollowUp",
                        principalColumn: "FollowUpId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_Lesion_LesionId",
                        column: x => x.LesionId,
                        principalTable: "Lesion",
                        principalColumn: "LesionId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_ModelPrediction_ModelPredictionId",
                        column: x => x.ModelPredictionId,
                        principalTable: "ModelPrediction",
                        principalColumn: "ModelPredictionId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "TreatmentId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_BiopsyId",
                table: "AuditLog",
                column: "BiopsyId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_DiagnosisId",
                table: "AuditLog",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_FinalDiagnosisId",
                table: "AuditLog",
                column: "FinalDiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_FollowUpId",
                table: "AuditLog",
                column: "FollowUpId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_ImageId",
                table: "AuditLog",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_LesionId",
                table: "AuditLog",
                column: "LesionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_ModelPredictionId",
                table: "AuditLog",
                column: "ModelPredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_PatientId",
                table: "AuditLog",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_TreatmentId",
                table: "AuditLog",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Authentication_UserId",
                table: "Authentication",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Biopsy_LesionId",
                table: "Biopsy",
                column: "LesionId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_BiopsyId",
                table: "Diagnosis",
                column: "BiopsyId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalDiagnosis_DiagnosisId",
                table: "FinalDiagnosis",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalDiagnosis_ModelPredictionId",
                table: "FinalDiagnosis",
                column: "ModelPredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUp_TreatmentId",
                table: "FollowUp",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_LesionId",
                table: "Images",
                column: "LesionId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesion_PatientId",
                table: "Lesion",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelPrediction_ImageId",
                table: "ModelPrediction",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_UserId",
                table: "Patient",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_FinalDiagnosisId",
                table: "Treatment",
                column: "FinalDiagnosisId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "Authentication");

            migrationBuilder.DropTable(
                name: "FollowUp");

            migrationBuilder.DropTable(
                name: "Treatment");

            migrationBuilder.DropTable(
                name: "FinalDiagnosis");

            migrationBuilder.DropTable(
                name: "Diagnosis");

            migrationBuilder.DropTable(
                name: "ModelPrediction");

            migrationBuilder.DropTable(
                name: "Biopsy");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Lesion");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
