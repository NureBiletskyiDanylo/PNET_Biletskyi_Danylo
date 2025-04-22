import { Genre } from "./genre"

export interface CreateBook{
    title: string
    description: string
    publicationDate: string
    bookGenres: Genre[]
}