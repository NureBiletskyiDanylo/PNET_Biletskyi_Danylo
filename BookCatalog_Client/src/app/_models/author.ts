import { Book } from "./book"

export interface Author{
    id: number
    name: string
    authorInfo: string
    books: Book[]
}