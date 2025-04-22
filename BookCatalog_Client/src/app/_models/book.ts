import { Genre } from "./genre"

export interface Book{
    id: number
    title: string
    description: string
    publicationDate: string
    bookGenres: Genre[]
    isFavourite: boolean
}