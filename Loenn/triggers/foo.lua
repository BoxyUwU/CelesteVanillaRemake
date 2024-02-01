local change_color_grade = {}

change_color_grade.name = "changeColorGrade"
change_color_grade.placements = {
    {
        name = "Snap Color Grade",
        fade = 0.0,
        data = {
            color_grade = "none",
        }
    },
    {
        name = "Fade Color Grade",
        data = {
            color_grade = "none",
            fade = 1.0,
        }
    },
}

return change_color_grade
