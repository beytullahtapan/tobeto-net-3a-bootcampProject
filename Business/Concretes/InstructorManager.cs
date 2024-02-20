using Business.Abstracts;
using Business.Requests.Instructors;
using Business.Responses.Instructors;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace Business.Concretes
{
    public class InstructorManager : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorManager(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }
        public async Task<List<GetAllInstructorResponse>> GetAll()
        {
            List<GetAllInstructorResponse> instructors = new List<GetAllInstructorResponse>();
            foreach (var instructor in await _instructorRepository.GetAll())
            {
                GetAllInstructorResponse response = new GetAllInstructorResponse {
                    UserId = instructor.Id,
                    CompanyName = instructor.CompanyName,
                };
                    instructors.Add(response);
            }
            return instructors;
        }

        public async Task<GetByIdInstructorResponse> GetById(int id)
        {
            
            Instructor instructor = await _instructorRepository.Get(x => x.Id == id);
            GetByIdInstructorResponse response = new GetByIdInstructorResponse {
                CompanyName = instructor.CompanyName,
            };
            return response;
        }

        public async Task<CreateInstructorResponse> AddAsync(CreateInstructorRequest request)
        {
            Instructor instructor = new Instructor {
                CompanyName = request.CompanyName,
                UserName = request.UserName,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalIdentity = request.NationalIdentity
             };
            await _instructorRepository.Add(instructor);

            CreateInstructorResponse response = new CreateInstructorResponse {
                CompanyName = request.CompanyName,
                Response = "Instructor Eklendi."

            };
            return response;
        }

        public async Task<DeleteInstructorResponse> DeleteAsync(DeleteInstructorRequest request)
        {
            Instructor instructor = new Instructor {
                Id = request.UserId
            };
            await _instructorRepository.Delete(instructor);

            DeleteInstructorResponse response = new DeleteInstructorResponse {
               UserId = request.UserId,
               Response = "Instructor Silindi."
            };
            return response;
        }

        public async Task<UpdateInstructorResponse> UpdateAsync(UpdateInstructorRequest request)
        {
            Instructor instructor = await _instructorRepository.Get(x => x.Id == request.UserId);
            instructor.CompanyName = request.CompanyName;
            instructor.UserName = request.UserName;
            instructor.FirstName = request.FirstName;
            instructor.LastName = request.LastName;
            instructor.Email = request.Email;
            instructor.Password = request.Password;
            instructor.DateOfBirth = request.DateOfBirth;
            instructor.NationalIdentity = request.NationalIdentity;
            await _instructorRepository.Update(instructor);

            UpdateInstructorResponse response = new UpdateInstructorResponse {
                UserId = request.UserId,
                CompanyName = request.CompanyName,
                Response = "Güncelleme Başarılı"
              };
            return response;
        }
    }
}
