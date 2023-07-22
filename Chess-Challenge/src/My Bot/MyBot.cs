using ChessChallenge.API;
using System;

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        Move[] legalMoves = board.GetLegalMoves();

        foreach (Move move in legalMoves)
        {
            if (MoveIsCheckmate(board, move))
            {
                return move;
            }
        }

        Random rng = new();

        return legalMoves[rng.Next(legalMoves.Length)];
    }

    bool MoveIsCheckmate(Board board, Move move)
    {
        board.MakeMove(move);
        bool isCheckmate = board.IsInCheckmate();
        board.UndoMove(move);

        return isCheckmate;
    }
}