using MeritoQuiz.Models;

namespace MeritoQuiz.Services;

public class QuestionService
{
    private List<Category> _categories = [];

    public async ValueTask<IEnumerable<Category>> GetCategories()
    {
        if (_categories.Count == 0)
            await FetchData();

        return _categories.AsEnumerable();
    }

    private Task FetchData()
    {
        _categories =
        [
            new Category()
            {
                Name = "Wiedza ogólna",
                Icon = "building",
                Questions =
                [
                    new Question
                    {
                        Text = "W którym roku powstała Wyższa Szkoła Bankowa w Gdańsku?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "1994",
                            },
                            new Answer
                            {
                                Text = "1998",
                                IsCorrect = true,
                            },
                            new Answer
                            {
                                Text = "2001",
                            },
                            new Answer
                            {
                                Text = "2005",
                            },
                        ],
                    },
                    new Question
                    {
                        Text = "Jak brzmi pełna aktualna nazwa uczelni po rebrandingu w 2023 roku?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "Akademia WSB",
                            },
                            new Answer
                            {
                                Text = "WSB Merito Gdańsk",
                                IsCorrect = true,
                            },
                            new Answer
                            {
                                Text = "Uniwersytet Merito",
                            },
                            new Answer
                            {
                                Text = "WSB Biznes School",
                            },
                        ],
                    },
                    new Question
                    {
                        Text = "Na jakiej ulicy znajduje się główny kampus WSB Merito w Gdańsku?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "Armii Krajowej",
                            },
                            new Answer
                            {
                                Text = "Grunwaldzka",
                            },
                            new Answer
                            {
                                Text = "Łąkowa",
                                IsCorrect = true,
                            },
                            new Answer
                            {
                                Text = "Dolna Brama",
                            },
                        ],
                    },
                    new Question
                    {
                        Text = "Jakie kolory dominują w identyfikacji wizualnej WSB Merito?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "Niebieski i biały",
                            },
                            new Answer
                            {
                                Text = "Czerwony i czarny",
                            },
                            new Answer
                            {
                                Text = "Zielony i żółty",
                            },
                            new Answer
                            {
                                Text = "Granatowy i pomarańczowy",
                                IsCorrect = true,
                            },
                        ],
                    },
                    new Question
                    {
                        Text =
                            "WSB Merito Gdańsk jest częścią większej sieci uczelni. Ile uczelni Merito działa w Polsce (stan na 2025)?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "8",
                            },
                            new Answer
                            {
                                Text = "10",
                            },
                            new Answer
                            {
                                Text = "11",
                                IsCorrect = true,
                            },
                            new Answer
                            {
                                Text = "12",
                            },
                        ],
                    },
                    new Question
                    {
                        Text = "Kto jest rektorem WSB Merito w Gdańsku (2024/2025)?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "dr hab. inż. Ewa Kulińska",
                            },
                            new Answer
                            {
                                Text = "dr Krzysztof Korda",
                                IsCorrect = true,
                            },
                            new Answer
                            {
                                Text = "dr Joanna Bogucka",
                            },
                            new Answer
                            {
                                Text = "prof. Andrzej Nowak",
                            },
                        ],
                    },
                    new Question
                    {
                        Text = "Jakie formy studiów oferuje uczelnia?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "tylko dzienne",
                            },
                            new Answer
                            {
                                Text = "dzienne i wieczorowe",
                            },
                            new Answer
                            {
                                Text = "stacjonarne i niestacjonarne",
                                IsCorrect = true,
                            },
                            new Answer
                            {
                                Text = "wyłącznie online",
                            },
                        ],
                    },
                    new Question
                    {
                        Text = "Jak nazywa się system, w którym studenci logują się do planu zajęć i ocen?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "Moodle",
                            },
                            new Answer
                            {
                                Text = "Extranet",
                                IsCorrect = true,
                            },
                            new Answer
                            {
                                Text = "USOS",
                            },
                            new Answer
                            {
                                Text = "EduPortal",
                            },
                        ],
                    },
                    new Question
                    {
                        Text = "Jakie miasto (poza Gdańskiem) należy do filii WSB Merito Gdańsk?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "Sopot",
                            },
                            new Answer
                            {
                                Text = "Elbląg",
                                IsCorrect = true,
                            },
                            new Answer
                            {
                                Text = "Gdynia",
                            },
                            new Answer
                            {
                                Text = "Malbork",
                            },
                        ],
                    },
                    new Question
                    {
                        Text = "Jak nazywa się uczelniany samorząd studencki?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "Parlament Studencki",
                            },
                            new Answer
                            {
                                Text = "Rada Studentów",
                            },
                            new Answer
                            {
                                Text = "Samorząd Studentów WSB Merito Gdańsk",
                                IsCorrect = true,
                            },
                            new Answer
                            {
                                Text = "Klub Studencki",
                            },
                        ],
                    },
                    new Question
                    {
                        Text =
                            "WSB Merito Gdańsk współpracuje z uczelniami w Europie. W ramach jakiego programu studenci mogą studiować za granicą?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "Erasmus+",
                                IsCorrect = true,
                            },
                            new Answer
                            {
                                Text = "Horizon",
                            },
                            new Answer
                            {
                                Text = "Comenius",
                            },
                            new Answer
                            {
                                Text = "Erasmus Old",
                            },
                        ],
                    },
                    new Question
                    {
                        Text = "Jakie kierunki należą do najpopularniejszych na WSB Merito Gdańsk?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "Informatyka, Zarządzanie, Finanse i Rachunkowość",
                                IsCorrect = true,
                            },
                            new Answer
                            {
                                Text = "Historia, Filologia, Psychologia",
                            },
                            new Answer
                            {
                                Text = "Architektura, Prawo, Fizyka",
                            },
                            new Answer
                            {
                                Text = "Chemia, Matematyka, Muzykologia",
                            },
                        ],
                    },
                    new Question
                    {
                        Text =
                            "Jak nazywa się wydarzenie organizowane dla nowych studentów na początku roku akademickiego?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "Otrzęsiny",
                            },
                            new Answer
                            {
                                Text = "Dzień Adaptacyjny",
                                IsCorrect = true,
                            },
                            new Answer
                            {
                                Text = "Inauguracja Roku",
                            },
                            new Answer
                            {
                                Text = "Merito Start",
                            },
                        ],
                    },
                    new Question
                    {
                        Text = "Jakie motto promocyjne towarzyszyło marce Merito po rebrandingu?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "Uczelnia przyszłości",
                            },
                            new Answer
                            {
                                Text = "Twoja kariera. Nasza pasja.",
                                IsCorrect = true,
                            },
                            new Answer
                            {
                                Text = "Merito – uczymy praktycznie",
                            },
                            new Answer
                            {
                                Text = "Z nami zbudujesz swoją przyszłość",
                            },
                        ],
                    },
                    new Question
                    {
                        Text = "Które z poniższych udogodnień można znaleźć na kampusie WSB Merito Gdańsk?",
                        Answers =
                        [
                            new Answer
                            {
                                Text = "Bibliotekę, bufet, strefę relaksu",
                                IsCorrect = true,
                            },
                            new Answer
                            {
                                Text = "Basen, kino, klub nocny",
                            },
                            new Answer
                            {
                                Text = "Tylko sale wykładowe",
                            },
                            new Answer
                            {
                                Text = "Laboratorium chemiczne",
                            },
                        ],
                    },
                ],
            },
            new Category()
            {
                Name = "Wiedza o bibliotece",
            },
            new Category()
            {
                Name = "Ciekawostki",
            },
        ];
        
        return Task.CompletedTask;
    }
}