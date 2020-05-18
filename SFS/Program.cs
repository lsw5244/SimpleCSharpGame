using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFS
{
    class Program
    {
        static char[,] board = new char[24, 50];  // 24,50
        static ConsoleKeyInfo ck;
        static int score = 0;
        static int maxscore = 0;

        static void Main(string[] args)
        {
            Random rnd = new Random();
            int x = 23;
            int y = 24;
            int flag = 0;    //맞았는지?
            int speed = 100;


            input();  //board 에 ' '넣음
            
                
            while (true)
            {

                write();   //중간 선 그음(판 + 스코어만듬)
                
                int r = rnd.Next(50);  //@넣을 랜덤값 만듬
                rock(r);  //@배열에 넣음 + board출력

                Thread.Sleep(speed);

                Console.Clear();

                for (int i = 23; i >= 0; i--)  //board의 @내리기 위함 + 끝까지가면 없애기
                {
                    for (int j = 0; j < 50; j++)
                    {
                        if (board[i, j] != ' ')   //@들어있나?
                        {
                            if (i < x)   //(x = 23) 24 50(23 49칸)   23보다 작다 == 한칸 내릴 수 있다.
                            {
                                board[i + 1, j] = board[i, j];     //한칸 아래로 내림
                                board[i, j] = ' ';  //원래있던거 지움
                            }
                            else
                            {
                                board[i, j] = ' ';  //있던거 지움
                                break;
                            }
                        }
                    }
                }

                if(board[x-1, y] == '@')  //부딪확인  플레이어 있을곳에 @있나? == 맞음
                {                         // 플레이어 x22에서 움직임
                    flag = 1;
                }

                //플레이어 움직일 부분
                if (Console.KeyAvailable)
                {
                    ck = Console.ReadKey(true);
                  
                }
                board[x, y] = ' ';  //원래 있던거 없앰

                switch (ck.Key)   //플레이어 움직이기
                {
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        if (board[x - 1, y - 1] == '@') { flag = 1; }  //왼쪽가다 맞음
                        y--;
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        if (board[x - 1, y + 1] == '@') { flag = 1; }  //오른쪽가다 맞음
                        y++;
                        break;
                    default: break;
                }

                if(y >= 49)  //플레이어 밖으로 나갔는지 확인
                {
                    y--;
                }
                if(y <= 0)
                {
                    y++;
                }

                board[x-1, y] = 'A';

                if (speed > 35)   //스피드 늘려줌 (35이하로 안내려가도록)
                {
                    speed -= 1;
                }
                score++;

                if (flag == 1)  //맞았을때
                {
                    Console.WriteLine("패배");
                    Console.WriteLine("종료하시려면 ESC키를 눌러주세요.");
                    ck = Console.ReadKey(true);
                    if(ck.Key != ConsoleKey.Escape)//다시시작하는코드
                    {
                        
                        if(score > maxscore)   //maxscore 확인
                        {
                            maxscore = score;
                        }
                        speed = 100;   //초기화
                        x = 23;
                        y = 24;
                        score = 0;
                        flag = 0;
                        input();   //board 다시초기화
                    }
                    else
                    {
                        break;  //종료
                    }
                }

           
            }
        }

        static void input()  //board초기화 해주는 메서드
        {
            for (int i = 0; i < 24; i++)  //board에 ' '넣음
            {
                for (int j = 0; j < 50; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }
        static void write()  //socre랑 선 출력
        {
            Console.SetWindowSize(50, 35);   //w50 x h35짜리 콘솔창으로 만듬
            Console.WriteLine("\n\n\n\n                    SCORE : " + score //9칸아래로 내림
                + "\n" + "                   MAXSCORE : " + maxscore + "\n\n\n");   // 스코어표시

            for (int i = 0; i < 50; i++)
            {
                Console.Write("-");   //10번째 행에 줄그음
            }
            Console.WriteLine();
        }

        static void rock(int r)  //@추가하는 메서드
        {
            board[0, r] = '@';   //새로운 곳에 @를 넣음

            for (int i = 0; i < 24; i++)  //@가 들어간board를 출력하는 문장
            {
                for (int j = 0; j < 50; j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}