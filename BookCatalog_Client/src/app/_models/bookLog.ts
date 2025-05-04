import { AuthorLog } from "./authorLog";

export interface BookLog {
    id: number;
    bookId: number;
    title: string;
    authorId: number;
    author: AuthorLog;
    insertedAt: string;
}