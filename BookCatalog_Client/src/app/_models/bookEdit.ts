import { Genre } from "./genre"

export interface BookEdit{
    id: number
    title: string
    description: string
    publicationDate: string
    bookGenres: Genre[]
}