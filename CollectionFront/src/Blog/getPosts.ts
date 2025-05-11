import {IPost} from "./Types";

const post1: IPost = {
    title: 'Sample post one',
    description: '# Sample 1 blog post #### April 1, 2020 by [Olivier](/)',
    date: '2024-10-06',
    image: '',
    imageLabel: ''
}
const post2: IPost = {
    title: 'Sample post two',
    description: '# Sample 2 blog post #### April 1, 2020 by [Olivier](/)',
    date: '2024-10-06',
    image: '',
    imageLabel: ''
}
const post3: IPost = {
    title: 'Sample post three',
    description: '# Sample 3 blog post #### April 1, 2020 by [Olivier](/)',
    date: '2024-10-06',
    image: '',
    imageLabel: ''
}

class GetPosts {
    get(): IPost[] {
        return [post1, post2, post3];
    }
}

const PostsProvider = new GetPosts();
export { PostsProvider };