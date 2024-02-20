using Business.Abstracts;
using Business.Requests.Applicants;
using Business.Responses.Applicants;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace Business.Concretes
{
    public class ApplicantManager : IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;
        public ApplicantManager(IApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        public async Task<List<GetAllApplicantResponse>> GetAll()
        {
            List<GetAllApplicantResponse> instructors = new List<GetAllApplicantResponse>();
            foreach (var applicant in await _applicantRepository.GetAll())
            {
                GetAllApplicantResponse response = new GetAllApplicantResponse {
                    UserId = applicant.Id,
                    About = applicant.About,
                    UserName = applicant.UserName,
                };
                instructors.Add(response);
            }
            return instructors;
        }

        public async Task<GetByIdApplicantResponse> GetById(int id)
        {
            Applicant applicant = await _applicantRepository.Get(x => x.Id == id);
            GetByIdApplicantResponse response = new GetByIdApplicantResponse {
                UserId = applicant.Id,
                About = applicant.About,
                UserName = applicant.UserName
            };
            return response;
        }

        public async Task<CreateApplicantResponse> AddAsync(CreateApplicantRequest request)
        {
            Applicant applicant = new Applicant { 
               About = request.About,
               UserName = request.UserName,
               Email = request.Email,
               FirstName = request.FirstName,
               LastName = request.LastName,
               DateOfBirth = request.DateOfBirth,
               Password = request.Password,
               NationalIdentity = request.NationalIdentity,
            }; 
            await _applicantRepository.Add(applicant);

            CreateApplicantResponse response = new CreateApplicantResponse {
                About = request.About,
                UserId = applicant.Id,
                Response = "Aplicant Eklendi."
            };
            return response;
        }

        public async Task<DeleteApplicantResponse> DeleteAsync(DeleteApplicantRequest request)
        {
            Applicant applicant = new Applicant {
                Id = request.UserId,
            };
            await _applicantRepository.Delete(applicant);

            DeleteApplicantResponse response = new DeleteApplicantResponse { 
                UserId = applicant.Id,
                Response = "Applicant Silindi."
            };
            return response;
        }

        public async Task<UpdateApplicantResponse> UpdateAsync(UpdateApplicantRequest request)
        {
            Applicant applicant = await _applicantRepository.Get(x => x.Id == request.UserId);
            if (applicant != null)
            {
                applicant.About = request.About;
                applicant.UserName = request.UserName;
                applicant.Email = request.Email;
                applicant.FirstName = request.FirstName;
                applicant.LastName = request.LastName;
                applicant.DateOfBirth = request.DateOfBirth;
                applicant.Password = request.Password;
                applicant.NationalIdentity = request.NationalIdentity;

                await _applicantRepository.Update(applicant);
            }

            UpdateApplicantResponse response = new UpdateApplicantResponse {
                UserId = request.UserId,
                UserName = request.UserName,
                Response = "Güncelleme Başarılı"
            };
            return response;
        }
    }
}
