using circutBoardWorkFlow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

namespace circutBoardWorkFlow.Tests
{
    public class CurcuitBoardServiceTests
    {
        private CircuitBoardService service;

        public CurcuitBoardServiceTests()
        {
            var options = new DbContextOptionsBuilder<CircuitBoardContext>()
                .UseInMemoryDatabase(databaseName: $"CircuitBoardContext_{Guid.NewGuid()}")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            service = new CircuitBoardService(new CircuitBoardContext(options));
        }

        [Fact]
        public async Task Test1()
        {
            var board = new CircuitBoardViewModel
            {
                Created = DateTimeOffset.Now,
                Status = Status.Registration
            };

            var result = await service.Create(board);

            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }


        [Fact]
        public async Task Test2()
        {
            var result = await service.Create(new CircuitBoardViewModel());

            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

            var board = result.Value!;
            board.Status = Status.ComponentInstallment;

            var nextResult = await service.Update(board.Id, board);

            Assert.NotNull(nextResult);
            Assert.True(nextResult.IsSuccess);
        }

        [Fact]
        public async Task Test3()
        {
            var result = await service.Create(new CircuitBoardViewModel());

            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

            var board = result.Value!;
            board.Status = Status.ComponentInstallment;

            var nextResult = await service.Update(board.Id, board);

            Assert.NotNull(nextResult);
            Assert.True(nextResult.IsSuccess);

            board.Status = Status.Registration;
            var badResult = await service.Update(board.Id, board);

            Assert.NotNull(badResult);
            Assert.True(badResult.IsFailure);
        }

        [Fact]
        public async Task Test4()
        {
            var result = await service.Create(new CircuitBoardViewModel());

            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

            var board = result.Value!;
            board.Status = Status.ComponentInstallment;

            var nextResult = await service.Update(board.Id, board);

            Assert.NotNull(nextResult);
            Assert.True(nextResult.IsSuccess);

            board.Status = Status.Registration;
            var badResult = await service.Update(board.Id, board);

            Assert.NotNull(badResult);
            Assert.True(badResult.IsFailure);

            var history = await service.GetHistory(board.Id);

            Assert.NotNull(history);
            Assert.True(history.IsSuccess);
            Assert.Single(history.Value!);
        }
    }
}